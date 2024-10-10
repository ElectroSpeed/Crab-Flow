using UnityEngine;
using UnityEngine.UI;

public class GetAudioSlider : MonoBehaviour
{
    public bool _isSfxSlider;
    public bool _isVolumeSlider;
    public bool _isMusicSlider;

    public Slider _audioSlider;

    public void Start()
    {
        if (_isSfxSlider)
        {
            AudioSettingsMenu.Instance._sfxSlider = _audioSlider;
            _audioSlider.value = AudioSettingsMenu.Instance._sfxVolume;
            _audioSlider.onValueChanged.AddListener(AudioSettingsMenu.Instance.SetSFXSound);
        }
        if (_isVolumeSlider)
        {
            AudioSettingsMenu.Instance._volumeSlider = _audioSlider;
            _audioSlider.value = AudioSettingsMenu.Instance._volumeVolume;
            _audioSlider.onValueChanged.AddListener(AudioSettingsMenu.Instance.SetVolumeSound);
        }
        if (_isMusicSlider)
        {
            AudioSettingsMenu.Instance._musicSlider = _audioSlider;
            _audioSlider.value = AudioSettingsMenu.Instance._musicVolume;
            _audioSlider.onValueChanged.AddListener(AudioSettingsMenu.Instance.SetMusicSound);
        }
    }
}
