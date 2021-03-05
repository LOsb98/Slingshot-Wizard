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

        //Two raycasts are used, one to check for grabbable surfaces and another for non-grabbable surfaces
        //Each uses a different layer mask
        RaycastHit2D grappleCheck = Physics2D.Raycast(grapplePos.position, (mousePos - grapplePos.position), grappleMax, grabLayer, 0);
        RaycastHit2D nonGrabCheck = Physics2D.Raycast(grapplePos.position, (mousePos - grapplePos.position), grappleMax, nonGrabLayer, 0);




        if (Input.GetMouseButtonDown(0))
        {
            //If a grabbable surface is in range but a non-grabbable surface is blocking it, the grapple won't attach
            //This is done by using both raycasts and checking which hits a surface first
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

        //grappleCheck and nonGrabCheck are passed into a separate method for readability purposes
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
            //If a grabbable surface is detected within range the end of the line will snap to it, prevents it from sticking out behind surfaces
            rope.SetPosition(1, grappleCheck.point);
            rope.startColor = okColor;
            rope.endColor = okColor;
        }
        else
        {
            //If nothing grabbable is in range the rope will point towards the mouse and maintain a fixed length
            //This gives better feedback than the old solution where the red line would follow the mouse no matter what
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
