using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using YG;
using Cells;
using Monetization;
using Tasks;
using NaughtyAttributes;

namespace BoardsRegulation
{
    public class GameFinisher : MonoBehaviour
    {
        [SerializeField] private CellsSearcher _cellsSearcher;
        [SerializeField] private TaskRegulator _taskRegulator;
        [SerializeField] private Reward _reward;
        [SerializeField] private GameObject _leaderboard;
        [SerializeField] private GameObject _gameOverBoard;
        [SerializeField] private GameObject _rewardButton;
        [SerializeField] private EventSystem _eventSystem;

        [Scene]
        [SerializeField] private string _sceneGame;
        [Scene]
        [SerializeField] private string _sceneMainMenu;

        public event Action GameIsOver;
        public event Action GameIsContinued;

        public void Initial()
        {
            _cellsSearcher.MovedInpossible += EndTheGame;
            _taskRegulator.MovesEnded += EndTheGame;
            _reward.Rewarded += ContinueTheGame;
            _reward.Rewarded += TurnOffRewardButton;
            YG2.onCloseAnyAdv += EnableEventSystem;
        }

        private void OnDisable()
        {
            _cellsSearcher.MovedInpossible -= EndTheGame;
            _taskRegulator.MovesEnded -= EndTheGame;
            _reward.Rewarded -= ContinueTheGame;
            _reward.Rewarded -= TurnOffRewardButton;
            YG2.onCloseAnyAdv -= EnableEventSystem;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(_sceneMainMenu);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(_sceneGame);
        }

        public void OpenLeaderboard()
        {
            _leaderboard.SetActive(true);
        }

        public void CloseLeaderboard()
        {
            _leaderboard.SetActive(false);
        }

        private void EndTheGame()
        {
            YG2.InterstitialAdvShow();
            _gameOverBoard.SetActive(true);
            GameIsOver?.Invoke();
        }

        private void EnableEventSystem()
        {
            _eventSystem.enabled = true;
        }

        private void ContinueTheGame()
        {
            _gameOverBoard.SetActive(false);
            GameIsContinued?.Invoke();
            Time.timeScale = 1f;
        }

        private void TurnOffRewardButton()
        {
            _rewardButton.SetActive(false);
        }
    }
}
