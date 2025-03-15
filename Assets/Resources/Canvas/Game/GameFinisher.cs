using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] CellsAnalyser _cellsAnalyser;
    [SerializeField] TaskRegulator _taskRegulator;
    [SerializeField] Reward _reward;
    [SerializeField] GameObject _leaderboard;
    [SerializeField] GameObject _gameOverBoard;

    public Action GameIsOver;
    public Action GameIsContinued;

    public void Initial()
    {
        _cellsAnalyser.MovedInpossible += EndTheGame;
        _taskRegulator.MovesEnded += EndTheGame;
        _reward.Rewarded += ContinueTheGame;
    }

    private void OnDisable()
    {
        _cellsAnalyser.MovedInpossible -= EndTheGame;
        _taskRegulator.MovesEnded -= EndTheGame;
        _reward.Rewarded -= ContinueTheGame;
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
        _gameOverBoard.SetActive(true);
        YandexGame.FullscreenShow();
        GameIsOver?.Invoke();
    }

    private void ContinueTheGame()
    {
        _gameOverBoard.SetActive(false);
        GameIsContinued?.Invoke();
    }
}
