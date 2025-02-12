using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] CellsAnalyser _cellsAnalyser;

    private void OnEnable()
    {
        _cellsAnalyser.MovedInpossible += EndTheGame;
    }

    private void EndTheGame()
    {
        Debug.Log("Game Over");
    }
}
