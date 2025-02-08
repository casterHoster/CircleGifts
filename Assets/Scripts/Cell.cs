using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent (typeof(AttentionMonitor))]
public class Cell : MonoBehaviour
{
    public Gift Gift { get; private set; }

    public void ReserveGift(Gift gift)
    {
        Gift = gift;
    }

    public void CleanGift()
    {
        Gift = null;
    }
}
