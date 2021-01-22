using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    private GameObject checkpoint;

    void Awake()
    {
        checkpoint = GameObject.Find("Checkpoint");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            print("Hit death plane");

            if (checkpoint.GetComponent<Checkpoint>().activated == true)
            {
                other.transform.position = checkpoint.transform.position;
                return;
            }
            other.transform.position = new Vector2(0, 0);
        }
    }
}
