using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Vector2 size;
    private Vector2 startPos;
    public float parallaxValue;
    public GameObject cam;

    private Vector3 oldCamPos;


    void Start()
    {
        oldCamPos = cam.transform.position;
        startPos = transform.localPosition;
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        Vector2 distance = cam.transform.position - oldCamPos;

        transform.localPosition -= new Vector3 (distance.x, distance.y, 0f) * parallaxValue;


        if (transform.localPosition.x >= size.x || transform.localPosition.x <= -size.x) transform.localPosition = new Vector3 (0, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.y >= size.y || transform.localPosition.y <= -size.y) transform.localPosition = new Vector3 (transform.localPosition.x, 0, transform.localPosition.z);

        print(transform.localPosition);


        oldCamPos = cam.transform.position;
    }
}
