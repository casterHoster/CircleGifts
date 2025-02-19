using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperViewer : MonoBehaviour
{
    [SerializeField] Wrapper _wrapper;
    [SerializeField] GiftsPool _giftsPool;

    private Gift _gift;
    private bool _isSet;

    public Action<Gift> ValueChanged;

    private void OnEnable()
    {
        _wrapper.Counted += SetUpGift;
    }

    private void SetUpGift(int value)
    {
        if (value == 0 && _isSet)
        {
            ValueChanged?.Invoke(_gift);
            _gift = null;
            _isSet = false;
        }
        else if (_gift != null)
        {
            ValueChanged?.Invoke(_gift);
            _gift = _giftsPool.GenerateForCanvas(value, transform);
            _isSet = true;
        }
        else
        {
            _gift = _giftsPool.GenerateForCanvas(value, transform);
            _isSet = true;
        }
    }
}
