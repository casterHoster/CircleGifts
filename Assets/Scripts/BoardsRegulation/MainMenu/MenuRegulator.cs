using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardsRegulation
{
    public class MenuRegulator : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _leaderboard;

        [Scene]
        [SerializeField] private string _sceneGame;

        public void OpenSettings()
        {
            _settings.SetActive(true);
            _mainMenu.SetActive(false);
        }

        public void OpenMenu()
        {
            _settings.SetActive(false);
            _leaderboard.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void OpenLeaderboard()
        {
            _leaderboard.SetActive(true);
            _mainMenu.SetActive(false);
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
