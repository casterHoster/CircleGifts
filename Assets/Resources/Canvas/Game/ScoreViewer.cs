using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreViewer : MonoBehaviour
{
    [SerializeField] ScoreCounter _scoreCounter;

    private TextMeshProUGUI _textMeshProUGUI;

    private void OnEnable()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        ChangeScoreView(_scoreCounter.Score);
        _scoreCounter.ScoreChanged += ChangeScoreView;
    }

    private void ChangeScoreView(int score)
    {
        _textMeshProUGUI.text = score.ToString();
    }
}
