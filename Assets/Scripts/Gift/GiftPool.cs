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
    private Cell _workingCell;

    public event Action<Gift> Instantiated;

    public void Initial()
    {
        _giftPool = new ObjectPool<Gift>
            (
            createFunc: () => _giftFabric.InstantGift(_board.RectTransform),
            actionOnGet: (obj) => GetGift(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            defaultCapacity: 25
            );

        _extractor.Extracted += ReleaseGift;
        _fieldCreator.CellCreated += AssignCell;
        _fieldCreator.CellCreated += Create;
    }

    private void OnDisable()
    {
        _fieldCreator.CellCreated -= Create;
        _fieldCreator.CellCreated -= AssignCell;
    }

    private void Create(Cell cell)
    {
        var gift = _giftPool.Get();
        gift.gameObject.transform.position =
            new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
        Instantiated?.Invoke(gift);
    }

    private void AssignCell(Cell cell)
    {
        _workingCell = cell;
    }

    private void ReleaseGift(Gift gift)
    {
        _giftPool.Release(gift);

        if (gift.transform.parent != null)
        {
            gift.transform.parent.gameObject.GetComponent<Cell>().PassEmpty();
        }
    }

    private void GetGift(Gift gift)
    {
        gift.gameObject.transform.position =
            new Vector3(_workingCell.transform.position.x, _workingCell.transform.position.y, gift.transform.position.z);
        gift.gameObject.SetActive(true);
    }
}
