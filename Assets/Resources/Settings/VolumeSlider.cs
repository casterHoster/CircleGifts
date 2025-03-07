using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;

    private Slider _slider;
    private float _defalutValue = 0.5f;

    public Action<string, float> OnValueChanged;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat(_soundType.Type, _defalutValue);
    }

    public void ChangeVolumeValue()
    {
        OnValueChanged?.Invoke(_soundType.Type, _slider.value);
    }
}
