using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GiftsGenerator : MonoBehaviour
{
    [SerializeField] private GiftsFabric _giftFabric;
    [SerializeField] private FieldCreator _fieldCreator;

    private ObjectPool<Gift> _giftPool;

    public event Action<Gift> Instantiated;

    public void Initial()
    {
        _fieldCreator.CellCreated += CreateIn;
    }

    private void CreateIn(Cell cell)
    {
        var gift = _giftFabric.InstantGift(cell.RectTransform);
        Instantiated?.Invoke(gift);
    }
}
