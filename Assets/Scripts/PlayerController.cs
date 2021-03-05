using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement movement;
    public LayerMask groundLayer;
    public Text speedText;
    public bool grounded;
    public bool boosting;
    public float boostTime;
    public float boostTimer;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize;

    void Update()
    {
        //Calculating the player's total speed using rb.velocity.magnitude to feed into the UI
        speedText.text = Mathf.RoundToInt(rb.velocity.magnitude).ToString();

        //Checking which direction the player is facing + whether they are on the ground
        DirectionCheck();
        GroundCheck();

        //Math.abs makes the value always positive, can use to find the player's velocity for animation purposes
        animator.SetFloat("MoveSpeed", System.Math.Abs(rb.velocity.x));

        //When the player hits a spring/boost tile their movement controls are briefly disabled
        //Prevents janky looking movement/increases boost consistency
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

    //Deciding which direction the player sprite should face based on which way the player is moving
    //TODO: Player sprite freaks out when trying to walk into a wall, probably want to detect whether the player is touching a wall + preventing movement inputs in that direction (overlapbox)
    private void DirectionCheck()
    { 
        if (GetComponent<DistanceJoint2D>().enabled == false)
        {
            if (rb.velocity.x < 0) transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (rb.velocity.x > 0) transform.localScale = new Vector3(1f, 1f, 1f);
            return;
        }
        if (GetComponent<DistanceJoint2D>().connectedAnchor.x > transform.position.x) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (GetComponent<DistanceJoint2D>().connectedAnchor.x < transform.position.x) transform.localScale = new Vector3(-1f, 1f, 1f);

    }

    public void Boost()
    {
        //Boost method called from the boost script, would be best to have everything to do with boost in a single method
        //Likely need to pass the boost values from the boost script to here
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
