using System;
using UnityEngine;

public class AudioManagerSave : MonoBehaviour
{
    public static AudioManagerSave Instance;

    public AudioSource _audioMusic;
    public AudioSource _audioSFX;

    public Sound[] _musicSounds;
    public Sound[] _sfxSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(_musicSounds, x => x._name == name);

        if (sound == null)
        {
            Debug.Log("Music Not Found");
        }
        else
        {
            _audioMusic.clip = sound._clip;
            _audioMusic.Play();
            Debug.Log(_audioMusic.clip.name);
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(_sfxSounds, x => x._name == name);

        if (sound == null)
        {
            Debug.Log("SFXSound Not Found");
        }
        else
        {
            _audioSFX.clip = sound._clip;
            _audioSFX.Play();
            Debug.Log(_audioSFX.clip.name);
        }
    }
}
