using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Animator animator;
    public DistanceJoint2D grapple;
    public LineRenderer rope;
    public Transform grapplePos;

    public LayerMask grabLayer;
    public LayerMask nonGrabLayer;
    public Vector3 mousePos;

    public float grappleMax;

    public Color ropeColor;
    public Color tooFarColor;
    public Color okColor;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //The Raycast used to check whether the grapple should be attached and to draw the rope
        RaycastHit2D grappleCheck = Physics2D.Raycast(grapplePos.position, (mousePos - grapplePos.position), grappleMax, grabLayer, 0);
        RaycastHit2D nonGrabCheck = Physics2D.Raycast(grapplePos.position, (mousePos - grapplePos.position), grappleMax, nonGrabLayer, 0);

        //grappleCheck is passed into a method to separate grapple attachment and drawing the rope, makes code more readable


        if (Input.GetMouseButtonDown(0))
        {
            //If a grabbable surface is in range but a non-grabbable surface is blocking it, the grapple won't attach
            if (grappleCheck && Vector2.Distance(nonGrabCheck.point, grapplePos.position) > Vector2.Distance(grappleCheck.point, grapplePos.position))
            {
                //If a grabbable surface is in range when the mouse is clicked, attach the grapple to it and set its max length
                grapple.connectedAnchor = grappleCheck.point;
                grapple.distance = Vector2.Distance(grapple.connectedAnchor, grapplePos.position);
                grapple.enabled = true;
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            //Disable the grapple when the mouse button is released
            grapple.enabled = false;
        }
        animator.SetFloat("GrappleHeight", grapple.connectedAnchor.y - grapplePos.position.y);
        animator.SetBool("Grappled", grapple.enabled);
        DrawRope(grappleCheck, nonGrabCheck);
    }

    private void DrawRope(RaycastHit2D grappleCheck, RaycastHit2D nonGrabCheck)
    {
        rope.SetPosition(0, grapplePos.position);
        if (grapple.enabled)
        {
            rope.startColor = ropeColor;
            rope.endColor = ropeColor;
            rope.SetPosition(1, grapple.connectedAnchor);
            //If the grapple is currently active the rope can be drawn and the method can be escaped, nothing else needs to be calculated
            return;
        }

        else if (grappleCheck && Vector2.Distance(nonGrabCheck.point, grapplePos.position) > Vector2.Distance(grappleCheck.point, grapplePos.position))
        {
            //If a grabbable surface is detected within range, the end of the line will snap to it
            rope.SetPosition(1, grappleCheck.point);
            rope.startColor = okColor;
            rope.endColor = okColor;
        }
        else
        {
            //Otherwise the line follows the mouse position until within range
            rope.SetPosition(1, ((mousePos - grapplePos.position).normalized * grappleMax) + grapplePos.position);
            rope.startColor = tooFarColor;
            rope.endColor = tooFarColor;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(grapplePos.position, mousePos);
    }
}
