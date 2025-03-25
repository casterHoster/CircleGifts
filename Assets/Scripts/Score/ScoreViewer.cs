using TMPro;
using UnityEngine;

namespace Score
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] ScoreCounter _scoreCounter;

        private TextMeshProUGUI _textMeshProUGUI;

        private void OnEnable()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _scoreCounter.ScoreChanged += ChangeScoreView;
        }

        private void ChangeScoreView(int score)
        {
            _textMeshProUGUI.text = score.ToString();
        }
    }
}
