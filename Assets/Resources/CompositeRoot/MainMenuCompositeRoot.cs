using UnityEngine;

public class MainMenuCompositeRoot : MonoBehaviour
{
    [SerializeField] private BackgroundSoundsRegulator _backgroundSoundsRegulator;
    [SerializeField] private VolumeSettings _volumeSettings;

    private void Start()
    {
        _backgroundSoundsRegulator.Initial();
        _volumeSettings.Initial();
    }
}
