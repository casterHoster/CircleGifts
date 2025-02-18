using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] Extractor _extractor;

    private int _score = 0;

    public Action<int> ScoreChanged;

    private void OnEnable()
    {
        _extractor.Scored += IncreaseScore;
    }

    private int CountCellsSum(List<Cell> cells)
    {
        return cells.Count * cells[cells.Count - 1].Gift.Value;
    }

    private void IncreaseScore(List<Cell> cells)
    {
        _score += CountCellsSum(cells);
        ScoreChanged?.Invoke(_score);
    }
}
