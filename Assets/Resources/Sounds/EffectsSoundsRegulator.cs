using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectsSoundsRegulator : MonoBehaviour
{
    [SerializeField] private AudioClip _wrapeEffect;
    [SerializeField] private AudioClip _extractEffect;
    [SerializeField] private Extractor _extractor;

    private AudioSource _audioSource;
    private float _pitchStep = 0.2f;
    private float _startedPitch = 1f;

    public void Initial()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = _startedPitch;
        _extractor.CellAdded += IncreasePitch;
        _extractor.CellRemoved += TakeAwayPitch;
        _extractor.Scored += UseExtractSound;
    }

    private void OnDisable()
    {
        _extractor.CellAdded -= IncreasePitch;
        _extractor.CellRemoved -= TakeAwayPitch;
        _extractor.Scored -= UseExtractSound;
    }

    private void UseWrapeSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = _wrapeEffect;
        _audioSource.Play();
    }

    private void UseExtractSound(List<Cell> cells)
    {
        ResetPitch();
        _audioSource.clip = _extractEffect;
        _audioSource.Play();
    }

    private void IncreasePitch(Cell cell)
    {
        _audioSource.pitch += _pitchStep;
        UseWrapeSound();
    }

    private void TakeAwayPitch()
    {
        _audioSource.pitch -= _pitchStep;
        UseWrapeSound();
    }

    private void ResetPitch()
    {
        _audioSource.pitch = _startedPitch;
    }
}
