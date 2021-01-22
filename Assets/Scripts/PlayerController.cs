using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement;
    public LayerMask groundLayer;
    public bool grounded;
    public bool boosting;
    public float boostTime;
    public float boostTimer;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize;

    void Update()
    {
        GroundCheck();
        if (boostTimer > 0) boostTimer -= Time.deltaTime;
        else boosting = false;

        if (Input.GetKey("space") && grounded)
        {
            movement.Jump();
        }

        if (Input.GetKey("d") && !boosting)
        {
            if (!grounded)
            {
                movement.AirMove(1);
                return;
            }
            movement.GroundMove(1);
            return;
        }

        if (Input.GetKey("a") && !boosting)
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

    public void Boost()
    {
        if (!boosting)
        {
            boosting = true;
            boostTimer = boostTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
