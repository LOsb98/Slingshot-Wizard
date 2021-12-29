using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSingletonObjects : MonoBehaviour
{
    [SerializeField] private GameObject _timerObject;
    [SerializeField] private GameObject _musicObject;
    [SerializeField] private GameObject _pauseObject;


    public void CreateNewTimer()
    {
        GameObject newTimer = Instantiate(_timerObject);

        DontDestroyOnLoad(newTimer);

        TimerController controller = TimerController.Instance;

        controller.SetTimerActive(true);
    }

    public void CreateNewMusicPlayer()
    {
        GameObject newMusic = Instantiate(_musicObject);

        DontDestroyOnLoad(newMusic);
    }

    public void CreateNewPauseManager()
    {
        GameObject newPauseManager = Instantiate(_pauseObject);

        DontDestroyOnLoad(newPauseManager);
    }

    public void ClearSingletonObjects()
    {  
        if (MusicController.Instance)
        {
            Destroy(MusicController.Instance.gameObject);
        }

        if (TimerController.Instance)
        {
            Destroy(TimerController.Instance.gameObject);
        }

        if (PauseManager.Instance)
        {
            Destroy(PauseManager.Instance.gameObject);
        }
    }

}
