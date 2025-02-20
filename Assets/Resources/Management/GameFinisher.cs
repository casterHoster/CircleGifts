using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] CellsAnalyser _cellsAnalyser;

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
        Debug.Log("Game Over");
    }
}
