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
        ChangeScorView(_scoreCounter.Score);
        _scoreCounter.ScoreChanged += ChangeScorView;
    }

    private void ChangeScorView(int score)
    {
        _textMeshProUGUI.text = score.ToString();
    }
}
