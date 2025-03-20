using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] Extractor _extractor;

    public Action<int> ScoreChanged;

    public int Score {  get; private set; }

    public void Initial()
    {
        _extractor.ListIsDefined += IncreaseScore;
    }

    private void OnDisable()
    {
        _extractor.ListIsDefined -= IncreaseScore;
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
