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

    private void IncreaseScore(List<Cell> cells)
    {
        foreach (Cell cell in cells) 
        {
            _score += cell.Gift.Value;
        }

        ScoreChanged?.Invoke(_score);
    }
}
