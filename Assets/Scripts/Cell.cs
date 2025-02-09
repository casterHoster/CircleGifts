using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent (typeof(AttentionMonitor))]
public class Cell : MonoBehaviour
{
    public Action<Cell> Cleared;

    public Gift Gift { get; private set; }

    public void Fill(Gift gift)
    {
        Gift = gift;
    }

    public void Clear()
    {
        Cleared?.Invoke(this);
    }
}
