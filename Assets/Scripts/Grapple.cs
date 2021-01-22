using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public DistanceJoint2D grapple;
    public LineRenderer rope;
    public LayerMask terrainLayer;
    public float grappleMax;
    public float grappleLength;
    public Vector3 targetPoint;
    public Color ropeColor;
    public Color tooFarColor;
    public Color okColor;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint.z = 0;

            RaycastHit2D grappleCheck = Physics2D.Raycast(transform.position, (targetPoint - transform.position), grappleMax, terrainLayer, 0);

            if (grappleCheck)
            {
                grapple.connectedAnchor = grappleCheck.point;
                grappleLength = Vector2.Distance(grapple.connectedAnchor, transform.position);
                grapple.distance = grappleLength;
                grapple.enabled = true;
            }
            else
            {
                grapple.enabled = false;
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            grapple.enabled = false;
        }
        DrawRope();
    }

    private void DrawRope()
    {
        if (grapple.enabled)
        {
            rope.startColor = ropeColor;
            rope.endColor = ropeColor; //Here
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, grapple.connectedAnchor);
            return;
        }
        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > grappleMax)
        {
            rope.startColor = tooFarColor;
            rope.endColor = tooFarColor; //Here
            return;
        }
        rope.startColor = okColor;
        rope.endColor = okColor; //Here

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPoint);
    }
}
