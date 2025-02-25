using System;
using UnityEngine;

public class WrapperViewer : MonoBehaviour
{
    [SerializeField] Wrapper _wrapper;
    [SerializeField] GiftsPool _giftsPool;

    private Gift _gift;
    private bool _isSetGift;

    public Action<Gift> ValueChanged;

    private void OnEnable()
    {
        _wrapper.Counted += SetUpGift;
    }

    private void SetUpGift(int value)
    {
        if (value == 0 && _isSetGift)
        {
            ValueChanged?.Invoke(_gift);
            _gift = null;
            _isSetGift = false;
        }
        else if (_gift != null)
        {
            ValueChanged?.Invoke(_gift);
            _gift = _giftsPool.GenerateForCanvas(value, transform);
            _isSetGift = true;
        }
        else
        {
            _gift = _giftsPool.GenerateForCanvas(value, transform);
            _isSetGift = true;
        }
    }
}
