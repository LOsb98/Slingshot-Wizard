using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowBestTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestTimeText;

    private void Start()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        string bestTimeString;

        if (bestTime > 0)
        {
            double minutes = System.Math.Truncate(bestTime / 60);

            double seconds = System.Math.Truncate(bestTime - (60 * minutes));

            string formattedSeconds = seconds.ToString("00");

            bestTimeString =  $"{minutes}:{formattedSeconds}";
        }
        else
        {
            bestTimeString = "No time set";
        }

        _bestTimeText.text = $"Best time: \n{bestTimeString}";
    }
}
