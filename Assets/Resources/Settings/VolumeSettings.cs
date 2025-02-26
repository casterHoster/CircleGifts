using System.Collections.Generic;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _effectsSource;
    [SerializeField] List<VolumeSlider> _sliders;

    const string MusicName = "Music";
    const string EffectsName = "Effects";

    private float _musicVolume;
    private float _effectsVolume;
    private float _defaultVolume = 0.5f;

    public void Initial()
    {
        foreach (VolumeSlider slider in _sliders) 
        {
            slider.OnValueChanged += SetVolume;
        }

        _musicVolume = PlayerPrefs.GetFloat(MusicName, _defaultVolume);
        _effectsVolume = PlayerPrefs.GetFloat(EffectsName, _defaultVolume);
        _musicSource.volume = _musicVolume;
        _effectsSource.volume = _effectsVolume;
    }

    private void OnDisable()
    {
        foreach (VolumeSlider slider in _sliders)
        {
            slider.OnValueChanged -= SetVolume;
        }
    }

    public void SetVolume(string type, float volume)
    {
        switch (type)
        {
            case MusicName:
                PlayerPrefs.SetFloat(MusicName, volume);
                _musicVolume = volume;
                _musicSource.volume = _musicVolume;
                break;

            case EffectsName:
                PlayerPrefs.SetFloat(EffectsName, volume);
                _effectsVolume = volume;
                _effectsSource.volume = _effectsVolume;
                break;
        }
    }
}
