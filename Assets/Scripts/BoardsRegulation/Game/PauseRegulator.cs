using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

namespace BoardsRegulation
{
    public class PauseRegulator : MonoBehaviour
    {
        [SerializeField] private GameObject _pause;
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _leaderboard;
        
        [Scene]
        [SerializeField] private string _sceneMainMenu;

        public event Action Paused;
        public event Action Resumed;

        private void Awake()
        {
            _pause.SetActive(false);
        }

        public void Resume()
        {
            _pause.SetActive(false);
            Time.timeScale = 1f;
            Resumed?.Invoke();
        }

        public void OpenPause()
        {
            Time.timeScale = 0f;
            _pause.SetActive(true);
            Paused?.Invoke();
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_sceneMainMenu);
        }

        public void OpenSettings()
        {
            _settings.SetActive(true);
            _pause.SetActive(false);
        }

        public void CloseSettings()
        {
            _settings.SetActive(false);
            _pause.SetActive(true);
        }

        public void OpenLeaderboard()
        {
            _pause.SetActive(false);
            _leaderboard.SetActive(true);
        }

        public void CloseLeaderboard()
        {
            _leaderboard.SetActive(false);
            _pause.SetActive(true);
        }
    }
}
