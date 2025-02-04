using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldCreator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private int _horizontalGiftsCount = 5;
    [SerializeField] private int _verticalGiftsCount = 5;
    [SerializeField] private float _backspaceX = 2.7f;
    [SerializeField] private float _backspaceY = 2.5f;
    [SerializeField] private int _space = 4;
    [SerializeField] private Cell _cellPrefab;

    private List<Vector2> _giftPositions;

    public Action<Cell> CellCreated;

    public void Initial()
    {
        _giftPositions = new List<Vector2>();
        FormPositions();

        for (int i = 0; i < _giftPositions.Count; i++)
        {
            var cell = Instantiate(_cellPrefab, _board.RectTransform);
            cell.RectTransform.anchoredPosition = _giftPositions[i];
            CellCreated?.Invoke(cell);
        }
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
}
