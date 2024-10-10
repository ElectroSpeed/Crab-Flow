using UnityEngine;
using UnityEngine.UI;

public class MaxMinVolumeAudio : MonoBehaviour
{
    public Slider _audioSlider;

    public static float _sliderValues;

    void Update()
    {
        _sliderValues = _audioSlider.value;

        if (_audioSlider.value == -30)
        {
            _audioSlider.minValue = -80;
            _audioSlider.value = _audioSlider.minValue;
        }
        else if (_audioSlider.minValue == -80 && _audioSlider.value != -80)
        {
            _audioSlider.minValue = -30;
            _audioSlider.value = _audioSlider.minValue + 1;
        }
    }
}