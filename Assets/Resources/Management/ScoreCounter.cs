using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] Extractor _extractor;

    public int Score {  get; private set; }

    public Action<int> ScoreChanged;

    public void Initial()
    {
        _extractor.Scored += IncreaseScore;
    }

    private void OnDisable()
    {
        _extractor.Scored -= IncreaseScore;
    }

    private void IncreaseScore(List<Cell> cells)
    {
        foreach (Cell cell in cells) 
        {
            Score += cell.Gift.Value;
        }

        ScoreChanged?.Invoke(Score);
    }
}
