using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

namespace BoardsRegulation
{
    public class PauseRegulator : MonoBehaviour
    { 
        [Scene]
        [SerializeField] private string _sceneMainMenu;

        public event Action Paused;
        public event Action Resumed;
        public event Action SettingsOpened;

        public void Resume()
        {
            
            Time.timeScale = 1f;
            Resumed?.Invoke();
        }

        public void OpenPause()
        {
            Time.timeScale = 0f;
            Paused?.Invoke();
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_sceneMainMenu);
        }

        public void OpenSettings()
        {
            SettingsOpened?.Invoke();
        }
    }
}
