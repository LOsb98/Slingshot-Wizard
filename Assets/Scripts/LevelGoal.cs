using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject endUI;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("Hit level end trigger");
            //Showing the end of level menu
            endUI.SetActive(true);

            //Disabling the player grapple and controls
            //Also stopping the rigidbody from being simulated so the player stops moving
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<DistanceJoint2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
