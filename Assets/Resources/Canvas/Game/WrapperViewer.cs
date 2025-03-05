using System;
using UnityEngine;

public class WrapperViewer : MonoBehaviour
{
    [SerializeField] Wrapper _wrapper;
    [SerializeField] GiftsPool _giftsPool;

    private Gift _gift;
    private bool _isSetGift;
    private RectTransform _rectTransform;


    public Action<Gift> ValueChanged;

    private void OnEnable()
    {
        Scaler scaler = new Scaler();
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = new Vector3(_rectTransform.localScale.x * scaler.Scaling, _rectTransform.localScale.y * scaler.Scaling);
        _wrapper.Counted += SetUpGift;
    }

    private void SetUpGift(int value, Cell cell)
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
            _gift = _giftsPool.GenerateForCanvas(value, transform, cell);
            _isSetGift = true;
        }
        else
        {
            _gift = _giftsPool.GenerateForCanvas(value, transform, cell);
            _isSetGift = true;
        }
    }
}
