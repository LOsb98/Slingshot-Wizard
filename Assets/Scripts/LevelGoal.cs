using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    public LevelLoader levelLoader;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("Hit level end trigger!");
            levelLoader.NextLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
