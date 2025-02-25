using UnityEngine;

public class MainMenuCompositeRoot : MonoBehaviour
{
    [SerializeField] private BackgroundSoundsRegulator _backgroundSoundsRegulator;

    private void Start()
    {
        _backgroundSoundsRegulator.Initial();
    }
}
