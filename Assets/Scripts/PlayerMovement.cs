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

    //Grounded movement currently uses addforce
    //May change this to directly setting velocity since boost now disables movement controls briefly
    public void GroundMove(Rigidbody2D rb, int direction)
    {
        //Using addforce means a max speed value + check must be set, otherwise the player would infinitely gain speed
        if (direction == 1 && rb.velocity.x > maxSpeed) return;
        if (direction == -1 && rb.velocity.x < maxSpeed * -1) return;
        rb.AddForce(new Vector2(direction * groundAccel, 0), ForceMode2D.Force);
    }

    //Air movement uses the same code as the ground move for now (i.e. both use addforce)
    public void AirMove(Rigidbody2D rb, int direction)
    {
        if (direction == 1 && rb.velocity.x > maxSpeed) return;
        if (direction == -1 && rb.velocity.x < maxSpeed * -1) return;
        rb.AddForce(new Vector2(direction * airAccel, 0), ForceMode2D.Force);
    }

    //Jumping is straightforward by itself
    //The jump boost needs to use a very low value as jumping currently uses Input.Getkey
    //Otherwise the player gets an extremely strong jump boost as it adds up for each frame the ground check box is active
    //Setting the jump boost higher and using Input.GetKeyDown would change how this works and require a more precise input from the player
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
