using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform _boardTransform;
    [SerializeField] private CellsFabric _cellFabric;
    [SerializeField] private int _horizontalCellsCount = 5;
    [SerializeField] private int _verticalCellsCount = 5;
    [SerializeField] private float _indentX = 2.7f;
    [SerializeField] private float _indentY = 2.5f;
    [SerializeField] private int _spacingCells = 4;

    public void Initial()
    {
        for (int x = 0; x < _horizontalCellsCount; x++)
        {
            for (int y = 0; y < _verticalCellsCount; y++)
            {
                var cell = _cellFabric.InstantCell(_boardTransform);
                cell.RectTransform.anchoredPosition = GetPositionOnBoard(x,y);
            }
        }
    }

    private Vector2 GetPositionOnBoard(int coordinateX, int coordinateY)
    {
        return new Vector2((-_boardTransform.rect.width / _indentX + _spacingCells * coordinateX),
            _boardTransform.rect.height / _indentY - _spacingCells * coordinateY);
    }
}
