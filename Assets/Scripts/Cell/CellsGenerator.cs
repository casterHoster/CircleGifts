using System;
using System.Collections.Generic;
using UnityEngine;

public class CellsGenerator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private CellsFabric _cellFabric;
    [SerializeField] private int _horizontalCellsCount = 5;
    [SerializeField] private int _verticalCellsCount = 5;
    [SerializeField] private float _backspaceX = 2.7f;
    [SerializeField] private float _backspaceY = 2.5f;
    [SerializeField] private int _space = 4;

    private List<Vector2> _cellPositions;

    public event Action<Cell> Instantiated;

    public void Initial()
    {
        _cellPositions = new List<Vector2>();
        FormCellsPositions();

            for (int i = 0; i < _cellPositions.Count; i++)
            {
                var cell = _cellFabric.InstantCell(_board.RectTransform);
                cell.RectTransform.anchoredPosition = _cellPositions[i];
                Instantiated?.Invoke(cell);
            }
    }

    private Vector2 GetPositionOnBoard(int coordinateX, int coordinateY)
    {
        return new Vector2((-_board.RectTransform.rect.width / _backspaceX + _space * coordinateX),
            _board.RectTransform.rect.height / _backspaceY - _space * coordinateY);
    }

    private void FormCellsPositions()
    {
        for (int x = 0; x < _horizontalCellsCount; x++)
        {
            for (int y = 0; y < _verticalCellsCount; y++)
            {
                _cellPositions.Add(GetPositionOnBoard(x,y));
            }
        }
    }
}
