using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Animator _animator;
    public DistanceJoint2D _grappleJoint;
    public LineRenderer _ropeLineRenderer;
    public Transform _grappleOrigin;

    public LayerMask _grabLayer;
    public LayerMask _nonGrabLayer;
    public Vector3 _mousePos;

    public float _grappleMaxLength;

    public Color _ropeColour;
    public Color _outOfGrappleRangeColor;
    public Color _inGrappleRangeColor;

    private void FixedUpdate()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0;
    }
    private void Update()
    {

        //Two raycasts are used, one to check for grabbable surfaces and another for non-grabbable surfaces
        //Each uses a different layer mask
        RaycastHit2D grappleCheck = Physics2D.Raycast(_grappleOrigin.position, (_mousePos - _grappleOrigin.position), _grappleMaxLength, _grabLayer, 0);
        RaycastHit2D nonGrabCheck = Physics2D.Raycast(_grappleOrigin.position, (_mousePos - _grappleOrigin.position), _grappleMaxLength, _nonGrabLayer, 0);




        if (Input.GetMouseButtonDown(0))
        {
            //If a grabbable surface is in range but a non-grabbable surface is blocking it, the grapple won't attach
            //This is done by using both raycasts and checking which hits a surface first
            if (grappleCheck && Vector2.Distance(nonGrabCheck.point, _grappleOrigin.position) > Vector2.Distance(grappleCheck.point, _grappleOrigin.position))
            {
                //If a grabbable surface is in range when the mouse is clicked, attach the grapple to it and set its max length
                _grappleJoint.connectedAnchor = grappleCheck.point;
                _grappleJoint.distance = Vector2.Distance(_grappleJoint.connectedAnchor, _grappleOrigin.position);
                _grappleJoint.enabled = true;
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            //Disable the grapple when the mouse button is released
            _grappleJoint.enabled = false;
        }
        _animator.SetFloat("GrappleHeight", _grappleJoint.connectedAnchor.y - _grappleOrigin.position.y);
        _animator.SetBool("Grappled", _grappleJoint.enabled);

        //grappleCheck and nonGrabCheck are passed into a separate method for readability purposes
        DrawRope(grappleCheck, nonGrabCheck);
    }

    private void DrawRope(RaycastHit2D grappleCheck, RaycastHit2D nonGrabCheck)
    {
        _ropeLineRenderer.SetPosition(0, _grappleOrigin.position);
        if (_grappleJoint.enabled)
        {
            _ropeLineRenderer.startColor = _ropeColour;
            _ropeLineRenderer.endColor = _ropeColour;
            _ropeLineRenderer.SetPosition(1, _grappleJoint.connectedAnchor);
            //If the grapple is currently active the _ropeLineRenderer can be drawn and the method can be escaped, nothing else needs to be calculated
            return;
        }

        else if (grappleCheck && Vector2.Distance(nonGrabCheck.point, _grappleOrigin.position) > Vector2.Distance(grappleCheck.point, _grappleOrigin.position))
        {
            //If a grabbable surface is detected within range the end of the line will snap to it, prevents it from sticking out behind surfaces
            _ropeLineRenderer.SetPosition(1, grappleCheck.point);
            _ropeLineRenderer.startColor = _inGrappleRangeColor;
            _ropeLineRenderer.endColor = _inGrappleRangeColor;
        }
        else
        {
            //If nothing grabbable is in range the _ropeLineRenderer will point towards the mouse and maintain a fixed length
            //This gives better feedback than the old solution where the red line would follow the mouse no matter what
            _ropeLineRenderer.SetPosition(1, ((_mousePos - _grappleOrigin.position).normalized * _grappleMaxLength) + _grappleOrigin.position);
            _ropeLineRenderer.startColor = _outOfGrappleRangeColor;
            _ropeLineRenderer.endColor = _outOfGrappleRangeColor;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_grappleOrigin.position, _mousePos);
    }
}
