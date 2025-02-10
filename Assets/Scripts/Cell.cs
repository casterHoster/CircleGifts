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
        Gift = gift;
        HasGift = true;
    }

    public void Clear()
    {
        Cleared?.Invoke(this);
        HasGift = false;
    }
}
