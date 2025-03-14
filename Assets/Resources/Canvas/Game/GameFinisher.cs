using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] CellsAnalyser _cellsAnalyser;
    [SerializeField] TaskRegulator _taskRegulator;
    [SerializeField] GameObject _gameOverBoard;

    public Action GameIsOver;

    public void Initial()
    {
        _cellsAnalyser.MovedInpossible += EndTheGame;
        _taskRegulator.MovesEnded += EndTheGame;
    }

    private void OnDisable()
    {
        _cellsAnalyser.MovedInpossible -= EndTheGame;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EndTheGame()
    {
        _gameOverBoard.SetActive(true);
        GameIsOver?.Invoke();
    }
}
