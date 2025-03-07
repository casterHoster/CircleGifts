using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] CellsAnalyser _cellsAnalyser;
    [SerializeField] GameObject _gameOverBoard;

    public void Initial()
    {
        _cellsAnalyser.MovedInpossible += EndTheGame;
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
    }
}
