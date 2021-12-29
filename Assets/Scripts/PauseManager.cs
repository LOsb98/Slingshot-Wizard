using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuUI;

    private static PauseManager _instance;

    public static PauseManager Instance { get { return _instance; } }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isGameBeingPaused = !_pauseMenuUI.activeInHierarchy;

            AudioSource audio = MusicController.Instance.AudioSource;

            if (isGameBeingPaused)
            {
                Time.timeScale = 0f;

                float currentVolume = audio.volume;

                float newVolume = (audio.volume / 2f);

                MusicController.Instance.ChangeMusicVolume(newVolume);
            }
            else
            {
                Time.timeScale = 1f;

                float regularVolume = PlayerPrefs.GetFloat("Volume");

                audio.volume = regularVolume;
            }

            _pauseMenuUI.SetActive(isGameBeingPaused);
        }
    }

    public void DestroySingletons()
    {
        Destroy(MusicController.Instance);
        Destroy(TimerController.Instance);
        Destroy(PauseManager.Instance);
    }
}
