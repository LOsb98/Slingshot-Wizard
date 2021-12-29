using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public AudioSource AudioSource => _audioSource;

    private static MusicController _instance;

    public static MusicController Instance { get { return _instance; } }

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

    private void Start()
    {
        _audioSource.volume = PlayerPrefs.GetFloat("Volume");
    }

    public void PauseMusic()
    {
        _audioSource.Pause();
    }

    public void PlayMusic()
    {
        _audioSource.Play();
    }

    public void ChangeMusicVolume(float newVolume)
    {
        _audioSource.volume = newVolume;
    }
}
