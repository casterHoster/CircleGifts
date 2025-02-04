using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GiftsGenerator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private GiftsFabric _GiftFabric;
    [SerializeField] private int _horizontalGiftsCount = 5;
    [SerializeField] private int _verticalGiftsCount = 5;
    [SerializeField] private float _backspaceX = 2.7f;
    [SerializeField] private float _backspaceY = 2.5f;
    [SerializeField] private int _space = 4;

    private List<Vector2> _giftPositions;
    private ObjectPool<Gift> _giftPool;

    public event Action<Gift> Instantiated;

    public void Initial()
    {
        _giftPool = new ObjectPool<Gift>
            (
            createFunc: () => _GiftFabric.InstantGift(_board.RectTransform)


            );

        _giftPositions = new List<Vector2>();
        FormPositions();

        for (int i = 0; i < _giftPositions.Count; i++)
            Create(_giftPositions[i]);

    }

    private Vector2 GetPositionOnBoard(int coordinateX, int coordinateY)
    {
        return new Vector2((-_board.RectTransform.rect.width / _backspaceX + _space * coordinateX),
            _board.RectTransform.rect.height / _backspaceY - _space * coordinateY);
    }

    private void FormPositions()
    {
        for (int x = 0; x < _horizontalGiftsCount; x++)
        {
            for (int y = 0; y < _verticalGiftsCount; y++)
            {
                _giftPositions.Add(GetPositionOnBoard(x, y));
            }
        }
    }

    private void Create(Vector2 giftPosition)
    {
        var gift = _GiftFabric.InstantGift(_board.RectTransform);
        gift.RectTransform.anchoredPosition = giftPosition;
        Instantiated?.Invoke(gift);
    }
}
