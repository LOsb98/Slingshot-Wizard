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

            //Make sure the checkpoint and spawnpos prefabs/objects have their Z value set to 0 or this warp will screw with the line renderer
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
