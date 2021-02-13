using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float groundAccel;
    public float airAccel;
    public float jumpBoost;
    public float jumpHeight;

    public void GroundMove(Rigidbody2D rb, int direction)
    {
        //Using AddForce on the ground makes player behave better with spring
        if (direction == 1 && rb.velocity.x > maxSpeed) return;
        if (direction == -1 && rb.velocity.x < maxSpeed * -1) return;
        rb.AddForce(new Vector2(direction * groundAccel, 0), ForceMode2D.Force);
    }

    public void AirMove(Rigidbody2D rb, int direction)
    {
        if (direction == 1 && rb.velocity.x > maxSpeed) return;
        if (direction == -1 && rb.velocity.x < maxSpeed * -1) return;
        rb.AddForce(new Vector2(direction * airAccel, 0), ForceMode2D.Force);
    }

    public void Jump(Rigidbody2D rb)
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x + jumpBoost, jumpHeight);
            return;
        }
        if (rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x - jumpBoost, jumpHeight);
            return;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
}
