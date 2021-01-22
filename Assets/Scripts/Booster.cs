using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public Vector2 boostDirection;

    public LayerMask playerLayer;
    public Vector2 boostArea;
    public float boostForce;

    void Update()
    {
        Collider2D boostCheck = Physics2D.OverlapBox(transform.position, boostArea, 0.0f, playerLayer);

        if (boostCheck)
        {
            boostCheck.GetComponent<Rigidbody2D>().AddForce(boostDirection * boostForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boostArea);
    }
}
