using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    public Gift Gift { get; private set; }

    public void ReserveGift(Gift gift)
    {
        Gift = gift;
    }
}
