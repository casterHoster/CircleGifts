using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardsRegulation
{
    public class MenuRegulator : MonoBehaviour
    {
        [Scene]
        [SerializeField] private string _sceneGame;

        public event Action MenuOpened;
        public event Action SettingsOpened;

        public void OpenSettings()
        {
            SettingsOpened?.Invoke();
        }

        public void OpenMenu()
        {
            MenuOpened?.Invoke();
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
