using UnityEngine;

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

    private void EndTheGame()
    {
        _gameOverBoard.SetActive(true);
    }
}
