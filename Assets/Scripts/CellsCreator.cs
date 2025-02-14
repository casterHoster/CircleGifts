using System;
using System.Collections.Generic;
using UnityEngine;

public class CellsCreator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private int _horizontalCellsCount = 5;
    [SerializeField] private int _verticalCellsCount = 5;
    [SerializeField] private float _backspaceBoardX = 2.7f;
    [SerializeField] private float _backspaceBoardY = 2.5f;
    [SerializeField] private float _spaceCells = 4;
    [SerializeField] private Cell _cellPrefab;

    private List<Vector2> _cellPositions;

    public Action<Cell> CellCreated;

    public void Initial()
    {
        _cellPositions = new List<Vector2>();
        FormPositions();
        GenerateCells();
    }

    private Vector2 GetStartedPositionOnBoard(int coordinateX, int coordinateY)
    {
        return new Vector2((-_board.RectTransform.rect.width / _backspaceBoardX + _spaceCells * coordinateX),
            _board.RectTransform.rect.height / _backspaceBoardY - _spaceCells * coordinateY);
    }

    private void FormPositions()
    {
        for (int x = 0; x < _horizontalCellsCount; x++)
        {
            for (int y = 0; y < _verticalCellsCount; y++)
            {
                _cellPositions.Add(GetStartedPositionOnBoard(x, y));
            }
        }
    }

    private void GenerateCells()
    {
        for (int i = 0; i < _cellPositions.Count; i++)
        {
            Cell cell = Instantiate(_cellPrefab, _board.RectTransform);
            RectTransform rectTransform = cell.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = _cellPositions[i];
            CellCreated?.Invoke(cell);
        }
    }
}
