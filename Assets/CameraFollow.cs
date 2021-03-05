using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    void Awake()
    {
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        print(rb.velocity.y);
        transform.position = player.transform.position + new Vector3(0f, 0f, -10f);
    }
}
