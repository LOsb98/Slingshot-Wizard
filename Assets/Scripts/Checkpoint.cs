using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool activated;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activated = true;
        }
    }
}
