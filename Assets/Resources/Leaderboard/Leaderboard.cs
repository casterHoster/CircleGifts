using System.Collections;
using UnityEngine;
using YG;
using YG.Utils.LB;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameFinisher _gameFinisher;
    [SerializeField] private ScoreCounter _scoreCounter;

    private WaitForSeconds _analyseDelay = new WaitForSeconds(0.1f);
    private string _leaderboardTechnicalName = "Leaderboard";

    public void Initial()
    {
        _gameFinisher.GameIsOver += RecordResult;
        YG2.onGetLeaderboard += StartCompareResult;
    }

    private void OnDisable()
    {
        _gameFinisher.GameIsOver -= RecordResult;
        YG2.onGetLeaderboard -= StartCompareResult;
    }

    private void RecordResult()
    {
        YG2.GetLeaderboard(_leaderboardTechnicalName);
    }

    private void StartCompareResult(LBData lBData)
    {
        StartCoroutine(CompareResult(lBData));
    }

    private IEnumerator CompareResult(LBData lBData)
    {
        yield return _analyseDelay;

        if (lBData.currentPlayer.score < _scoreCounter.Score)
        {
            YG2.SetLeaderboard(_leaderboardTechnicalName, _scoreCounter.Score);
        }
    }
}
