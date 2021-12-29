using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] doors;
    public float openTime;

    private bool _buttonIsActive;

    public void Activate()
    {
        print("Button hit");

        if (_buttonIsActive)
        {
            return;
        }

        foreach (GameObject door in doors)
        {
            door.SetActive(false);

            _buttonIsActive = true;

            Invoke("ReactivateDoors", openTime);
        }
    }

    private void ReactivateDoors()
    {
        foreach (GameObject door in doors) door.SetActive(true);

        _buttonIsActive = false;
    }
}
