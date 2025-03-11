using UnityEngine;
using YG;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameFinisher _gameFinisher;
    [SerializeField] ScoreCounter _scoreCounter;

    public void Initial()
    {
        _gameFinisher.GameIsOver += RecordResult;
    }

    private void RecordResult()
    {
        YandexGame.NewLeaderboardScores("Leaderboard", _scoreCounter.Score);
    }
}
