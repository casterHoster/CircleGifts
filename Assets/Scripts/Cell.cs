using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent (typeof(AttentionMonitor))]
public class Cell : MonoBehaviour
{
    public Action<Cell> Cleared;

    public bool HasGift {  get; private set; } 

    public Gift Gift { get; private set; }

    public void Fill(Gift gift)
    {
        if (gift != null)
        {
            Gift = gift;
            HasGift = true;
        }
        else
        {
            Debug.LogError("Gift is null!");
        }
    }

    public void Clear()
    {
        Gift = null;
        HasGift = false;
        Cleared?.Invoke(this);
    }
}
