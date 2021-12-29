using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float _currentTimer;

    public float CurrentTimer => _currentTimer;

    [SerializeField] private TextMeshProUGUI _timerText;

    private static TimerController _instance;

    public static TimerController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetTimerActive(bool activeState)
    {
        this.enabled = activeState;
    }

    private void Update()
    {
        _currentTimer += Time.deltaTime;


        _timerText.text = GetFormattedTime(_currentTimer);
    }

    public void SetFinalTime()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (bestTime > _currentTimer || bestTime <= 0)
        {
            Debug.Log($"Set new best time: {_currentTimer}");

            PlayerPrefs.SetFloat("BestTime", _currentTimer);
        }
        else
        {
            Debug.Log("Did not set new best time");
        }
        
    }

    public string GetFormattedTime(float timeToFormat)
    {
        double minutes = System.Math.Truncate(timeToFormat / 60);

        double seconds = System.Math.Truncate(timeToFormat - (60 * minutes));

        string formattedSeconds = seconds.ToString("00");

        string finalString = $"{minutes} : {formattedSeconds}";

        return finalString;
    }
}
