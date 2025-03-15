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

    private void GiveReward(int id)
    {
        if (id == 1)
            Rewarded?.Invoke();
    }

    public void OpenRewardAd()
    {
        YandexGame.RewVideoShow(continueRewardIndex);
    }
}
