using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public LayerMask groundLayer;
    public bool grounded;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize;

    void Update()
    {
        GroundCheck();
        if (Input.GetKey("space") && grounded)
        {
            movement.Jump();
        }

        if (Input.GetKey("d"))
        {
            if (!grounded)
            {
                movement.AirMove(1);
                return;
            }
            movement.GroundMove(1);
            return;
        }

        if (Input.GetKey("a"))
        {
            if (!grounded)
            {
                movement.AirMove(-1);
                return;
            }
            movement.GroundMove(-1);
            return;
        }
    }

    private void GroundCheck()
    {
        grounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0.0f, groundLayer);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
