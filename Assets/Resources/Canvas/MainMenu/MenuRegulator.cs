using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRegulator : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settings;

    public void OpenSettings()
    {
        _settings.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void OpenMenu()
    {
        _settings.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
