using UnityEngine;
using Settings;
using Sounds;
using BoardsRegulation;

public class MainMenuCompositeRoot : MonoBehaviour
{
    [SerializeField] private BackgroundSoundsRegulator _backgroundSoundsRegulator;
    [SerializeField] private VolumeSettings _volumeSettings;
    [SerializeField] private MenuBoardsRegulator _menuBoardsRegulator;

    private void Start()
    {
        _backgroundSoundsRegulator.Initial();
        _volumeSettings.Initial();
        _menuBoardsRegulator.Initial();
    }
}
