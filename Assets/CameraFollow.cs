using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = GameObject.Find("Player").transform.position + new Vector3(0f, 0f, -10f);
    }
}
