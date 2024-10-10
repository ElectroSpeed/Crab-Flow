using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{
    public AudioMixer _audioMixer;

    public Slider _sfxSlider;
    public Slider _musicSlider;
    public Slider _volumeSlider;

    public float _sfxVolume;
    public float _musicVolume;
    public float _volumeVolume;

    public static AudioSettingsMenu Instance;

    private void Awake()
    {
        if (Instance != this || Instance == null)
        {
            Instance = this;
        }
    }

    public void SetMusicSound(float volume)
    {
        _audioMixer.SetFloat("Music", volume);
        _musicVolume = volume;
    }

    public void SetSFXSound(float volume)
    {
        _audioMixer.SetFloat("SFX", volume);
        _sfxVolume = volume;
    }

    public void SetVolumeSound(float volume)
    {
        _audioMixer.SetFloat("Master", volume);
        _volumeVolume = volume;
    }
}
