using System;
using UnityEngine;
using YG;

public class Reward : MonoBehaviour
{
    private int continueRewardIndex = 1;

    public Action Rewarded;

    public void Initial()
    {
        YandexGame.RewardVideoEvent += GiveReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GiveReward;
    }

    public void OpenRewardAd()
    {
        YandexGame.RewVideoShow(continueRewardIndex);
    }

    private void GiveReward(int id)
    {
        if (id == 1)
            Rewarded?.Invoke();
    }
}
