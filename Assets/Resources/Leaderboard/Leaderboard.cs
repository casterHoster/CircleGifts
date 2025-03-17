using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using YG;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameFinisher _gameFinisher;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private LeaderboardYG _leaderboardYG;

    public void Initial()
    {
        _gameFinisher.GameIsOver += RecordResult;
    }

    private void OnDisable()
    {
        _gameFinisher.GameIsOver -= RecordResult;
    }

    private void RecordResult()
    {
        YandexGame.NewLeaderboardScores("Leaderboard", _scoreCounter.Score);
        _leaderboardYG.UpdateLB();
    }
}
