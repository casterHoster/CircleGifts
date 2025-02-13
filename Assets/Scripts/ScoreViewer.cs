using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreViewer : MonoBehaviour
{
    [SerializeField] ScoreCounter _scoreCounter;

    private TextMeshProUGUI _textMeshProUGUI;
    private int _scoreView;

    private void OnEnable()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _scoreCounter.ScoreChanged += ChangeScorView;
    }



    private void ChangeScorView(int score)
    {
        _textMeshProUGUI.text = score.ToString();
    }
}
