using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundSoundsRegulator : MonoBehaviour
{
    [SerializeField] private AudioClip _backgroundMusic;

    private AudioSource _audioSource;

    public void Initial()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _backgroundMusic;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
