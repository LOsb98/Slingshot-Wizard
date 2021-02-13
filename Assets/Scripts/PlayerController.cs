using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
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
        DirectionCheck();
        GroundCheck();
        animator.SetFloat("MoveSpeed", System.Math.Abs(rb.velocity.x));
        if (boostTimer > 0) boostTimer -= Time.deltaTime;
        else boosting = false;

        if (Input.GetKey("space") && grounded)
        {
            movement.Jump(rb);
        }

        if (Input.GetKey("d") && !boosting)
        {
            if (!grounded)
            {
                movement.AirMove(rb, 1);
                return;
            }
            movement.GroundMove(rb, 1);
            animator.SetBool("Moving", true);
            return;
        }

        if (Input.GetKey("a") && !boosting)
        {
            if (!grounded)
            {
                movement.AirMove(rb , - 1);
                return;
            }
            movement.GroundMove(rb , - 1);
            animator.SetBool("Moving", true);
            return;
        }
        animator.SetBool("Moving", false);
    }

    private void GroundCheck()
    {
        grounded = Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0.0f, groundLayer);
        animator.SetBool("Airborne", !grounded);
    }

    private void DirectionCheck()
    {
        if (GetComponent<DistanceJoint2D>().enabled == false)
        {
            if (rb.velocity.x < 0) transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (rb.velocity.x > 0) transform.localScale = new Vector3(1f, 1f, 1f);
            return;
        }
        if (GetComponent<DistanceJoint2D>().connectedAnchor.x > transform.position.x) transform.localScale = new Vector3(1f, 1f, 1f);
        else transform.localScale = new Vector3(-1f, 1f, 1f);

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
