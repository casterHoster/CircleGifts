using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GiftsPool : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private GiftsFabric _giftFabric;
    [SerializeField] private FieldCreator _fieldCreator;
    [SerializeField] private Extractor _extractor;

    private ObjectPool<Gift> _giftPool;
    private Cell _currentCell;

    public void Initial()
    {
        _giftPool = new ObjectPool<Gift>
            (
            createFunc: () => _giftFabric.InstantGift(_board.RectTransform),
            actionOnGet: (obj) => GetFromPool(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            defaultCapacity: 25
            );

        _extractor.Extracted += ReleaseGift;
        _fieldCreator.CellCreated += CreateProcess;
    }

    private void OnDisable()
    {
        _fieldCreator.CellCreated -= CreateProcess;
    }

    private void CreateProcess(Cell cell)
    {
        _currentCell = cell;
        var gift = _giftPool.Get();
        gift.gameObject.transform.position =
            new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
        cell.Fill(gift);
    }

    private void ReleaseGift(Cell cell)
    {
        _giftPool.Release(cell.Gift);
    }

    private void GetFromPool(Gift gift)
    {
        gift.gameObject.transform.position =
            new Vector3(_currentCell.transform.position.x, _currentCell.transform.position.y, gift.transform.position.z);
        gift.gameObject.SetActive(true);
    }
}
