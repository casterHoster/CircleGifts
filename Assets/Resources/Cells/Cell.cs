using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent (typeof(AttentionMonitor))]
[RequireComponent(typeof (BoxCollider2D))]
public class Cell : MonoBehaviour
{
    //private BoxCollider2D _boxCollider;

    public Action<Cell> Cleared;

    public Gift Gift { get; private set; }

    //private void Start()
    //{
    //    _boxCollider = GetComponent<BoxCollider2D>();
    //}

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


    //public void TurnBoxCollider2D()
    //{
    //    if (_boxCollider.enabled == true)
    //    {
    //        _boxCollider.enabled = false;
    //    }
    //    else
    //    {
    //        _boxCollider.enabled = true;
    //    }
    //}
}
