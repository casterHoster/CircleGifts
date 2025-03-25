using System;
using UnityEngine;
using Gifts;
using UnityEngine.UI;

namespace Cells
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(AttentionMonitor))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Image))]
    public class Cell : MonoBehaviour
    {
        public event Action<Cell> Cleared;

        public Gift Gift { get; private set; }

        public Image ImageLight {  get; private set; }

        private void Awake()
        {
            ImageLight = GetComponent<Image>();
        }

        public void Fill(Gift gift)
        {
            if (gift != null)
            {
                Gift = gift;
            }
        }

        public void ClearGift()
        {
            Gift = null;
            Cleared?.Invoke(this);
        }
    }
}
