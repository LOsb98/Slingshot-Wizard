using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public Vector2 boostDirection;

    public LayerMask playerLayer;
    public Vector2 boostArea;
    public Transform boostCheckPos;
    public float boostForce;
    public Animator animator;
    public float delayTime;
    private float delayTimer;

    void Update()
    {
        Collider2D boostCheck = Physics2D.OverlapBox(boostCheckPos.position, boostArea, 0.0f, playerLayer);

        if (delayTimer > 0) delayTimer -= Time.deltaTime;

        if (delayTimer <= 0 && boostCheck && boostCheck.tag == "Player")
        {
            //Maybe change how PlayerController/PlayerMovement work to reduce this to one method call
            boostCheck.GetComponent<Rigidbody2D>().velocity = (boostDirection * boostForce);
            boostCheck.GetComponent<PlayerController>().Boost();
            delayTimer = delayTime;
            animator.SetTrigger("BoostHit");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boostCheckPos.position, boostArea);
    }
}
