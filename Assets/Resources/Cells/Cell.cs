using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent (typeof(AttentionMonitor))]
[RequireComponent(typeof (BoxCollider2D))]
public class Cell : MonoBehaviour
{
    public Action<Cell> Cleared;

    public Gift Gift { get; private set; }

    public void Fill(Gift gift)
    {
        if (gift != null)
        {
            Gift = gift;
        }
    }

    public void Clear()
    {
        Gift = null;
        Cleared?.Invoke(this);
    }
}
