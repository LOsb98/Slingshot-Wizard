using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalGoal : LevelGoal
{
    [SerializeField] private TextMeshProUGUI _finalTimeText;

    protected override void FinishLevel()
    {
        base.FinishLevel();

        //Put final time into 

        if (TimerController.Instance != null)
        {
            TimerController.Instance.SetTimerActive(false);

            TimerController.Instance.SetFinalTime();

            float finalTime = TimerController.Instance.CurrentTimer;

            string formattedTimeString = TimerController.Instance.GetFormattedTime(finalTime);

            _finalTimeText.text = $"Final time: \n {formattedTimeString}";
        }
        else
        {
            _finalTimeText.text = "Have a go at time trial mode?";
        }
        
    }
}
