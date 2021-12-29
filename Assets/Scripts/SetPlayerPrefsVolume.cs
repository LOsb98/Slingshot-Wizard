using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerPrefsVolume : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void SetNewVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("Volume", newVolume);
    }
}
