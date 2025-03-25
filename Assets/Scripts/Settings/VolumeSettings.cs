using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Settings
{
    public class VolumeSettings : MonoBehaviour
    {
        public const string MusicName = "Music";
        public const string EffectsName = "Effects";

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;
        [SerializeField] private List<VolumeSlider> _sliders;

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
            YG2.onShowWindowGame += TurnOnVolume;
            YG2.onHideWindowGame += TurnOffVolume;
        }

        private void OnDisable()
        {
            foreach (VolumeSlider slider in _sliders)
            {
                slider.OnValueChanged -= SetVolume;
            }

            YG2.onShowWindowGame -= TurnOnVolume;
            YG2.onHideWindowGame -= TurnOffVolume;
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

        private void TurnOnVolume()
        {
            _musicSource.enabled = true;
            _effectsSource.enabled = true;
        }

        private void TurnOffVolume()
        {
            _musicSource.enabled = false;
            _effectsSource.enabled = false;
        }
    }
}
