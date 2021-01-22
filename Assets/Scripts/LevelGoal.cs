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
            endUI.SetActive(true);

            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}
