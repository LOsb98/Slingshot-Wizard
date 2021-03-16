using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] doors;
    public float openTime;

    public float timer;
    public float Timer
    {
        get { return timer; }
        set 
        {
            timer = value;
            if (timer <= 0)
            {
                foreach (GameObject door in doors) door.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (Timer > 0) Timer -= Time.deltaTime;
    }

    public void Activate()
    {
        print("Button hit");
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
            Timer = openTime;
        }
    }
}
