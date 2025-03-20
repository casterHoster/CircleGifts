using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using YG;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] CellsSearcher _cellsSearcher;
    [SerializeField] TaskRegulator _taskRegulator;
    [SerializeField] Reward _reward;
    [SerializeField] GameObject _leaderboard;
    [SerializeField] GameObject _gameOverBoard;
    [SerializeField] GameObject _rewardButton;
    [SerializeField] EventSystem _eventSystem;

    public Action GameIsOver;
    public Action GameIsContinued;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
