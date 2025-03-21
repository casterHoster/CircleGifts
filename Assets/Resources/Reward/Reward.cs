using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class Reward : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;

    private string rewardID = "1";

    public Action Rewarded;

    public void Initial()
    {
        YG2.onCloseRewardedAdv += EnableEventSystem;
    }

    public void OpenRewardAd()
    {
        YG2.RewardedAdvShow(rewardID, () =>
        {
            if (rewardID == "1")
                Rewarded?.Invoke();

            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            eventSystem.enabled = true;
        });
    }

    private void EnableEventSystem()
    {
        _eventSystem.enabled = true;
    }
}
