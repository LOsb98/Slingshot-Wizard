using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpBoost;

    public void GroundMove(int direction)
    {
        rb.velocity = new Vector2((direction * speed), rb.velocity.y);
    }

    public void AirMove(int direction)
    {
        if (direction == 1 && rb.velocity.x > speed) return;
        if (direction == -1 && rb.velocity.x < speed * -1) return;
        rb.AddForce(new Vector2(direction * (speed / 2), 0), ForceMode2D.Force);
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

    public void Boost(int direction)
    {

    }
}
