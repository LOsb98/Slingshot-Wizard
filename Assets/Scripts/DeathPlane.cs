using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    private GameObject checkpoint;
    private GameObject spawnPos;

    void Awake()
    {
        checkpoint = GameObject.Find("Checkpoint");
        spawnPos = GameObject.Find("SpawnPos");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            other.GetComponent<DistanceJoint2D>().enabled = false;
            print("Hit death plane");

            //This will fuck up the line renderer if the player is sent to an object with a Z coordinate that isn't 0
            if (checkpoint.GetComponent<Checkpoint>().activated == true)
            {
                other.transform.position = checkpoint.transform.position;
            }
            else
            {
                other.transform.position = spawnPos.transform.position;
            }
        }
    }
}
