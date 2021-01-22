using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed;
    public float groundAccel;
    public float airAccel;
    public float jumpBoost;

    public void GroundMove(int direction)
    {
        //Using AddForce on the ground makes player behave better with spring
        if (direction == 1 && rb.velocity.x > maxSpeed) return;
        if (direction == -1 && rb.velocity.x < maxSpeed * -1) return;
        rb.AddForce(new Vector2(direction * groundAccel, 0), ForceMode2D.Force);
    }

    public void AirMove(int direction)
    {
        if (direction == 1 && rb.velocity.x > maxSpeed) return;
        if (direction == -1 && rb.velocity.x < maxSpeed * -1) return;
        rb.AddForce(new Vector2(direction * airAccel, 0), ForceMode2D.Force);
    }

    public void Jump()
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x + jumpBoost, rb.velocity.y);
        }
        if (rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x - jumpBoost, rb.velocity.y);
        }
        rb.velocity = new Vector2(rb.velocity.x, 6);
    }
}
