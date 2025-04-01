using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

namespace Monetization
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;

        private string _rewardID = "1";

        public event Action Rewarded;

        public void OpenRewardAd()
        {
            YG2.RewardedAdvShow(_rewardID, () =>
            {
                if (_rewardID == "1")
                    Rewarded?.Invoke();

                EventSystem eventSystem = FindObjectOfType<EventSystem>();
                eventSystem.enabled = true;
            });
        }
    }
}
