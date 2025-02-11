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
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            defaultCapacity: 25
            );

        _extractor.Extracted += ReleaseGift;
        _fieldCreator.CellCreated += Generate;
        _giftFabric.Maximised += ReleaseGift;
    }

    private void OnDisable()
    {
        _fieldCreator.CellCreated -= Generate;
    }

    public void Generate(Cell cell)
    {
        var gift = _giftPool.Get();
        cell.Fill(gift);
        _giftFabric.Characterize(gift);
        gift.gameObject.SetActive(true);
        gift.gameObject.transform.position =
            new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
    }

    private void ReleaseGift(Cell cell)
    {
        _giftPool.Release(cell.Gift);
        cell.Clear();
    }
}
