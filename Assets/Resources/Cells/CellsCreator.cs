using System;
using System.Collections.Generic;
using UnityEngine;

public class CellsCreator : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Board _board;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 5;

    private List<Vector2> _cellPositions;
    private float _screenWidth;
    private float _screenHeight;
    private float _cellSpaceWidth;
    private float _cellSpaceHeight;
    private float _cellScaling;
    private Vector3 _prefabCellSize;
    private float _countEmptySpaceInCells = 2;

    public Action<Cell> CellCreated;

    public void Initial()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        CalculateCellScaling();
        _prefabCellSize = _cellPrefab.transform.localScale;
        _cellSpaceWidth = _cellScaling * _prefabCellSize.x * _countEmptySpaceInCells;
        _cellSpaceHeight = _cellScaling * _prefabCellSize.y * _countEmptySpaceInCells;
        _cellPositions = new List<Vector2>();
        FormPositions();
        GenerateCells();
    }

    private Vector2 GetPositionOnBoard(int coordinateX, int coordinateY)
    {
        float halfWidth = (_columns - 1) * _cellSpaceWidth / 2f;
        float halfHeight = (_rows - 1) * _cellSpaceHeight / 2f;
        float positionX = coordinateX * _cellSpaceWidth - halfWidth;
        float positionY = -coordinateY * _cellSpaceHeight + halfHeight;

        return new Vector2(positionX, positionY);
    }

    private void FormPositions()
    {
        for (int x = 0; x < _rows; x++)
        {
            for (int y = 0; y < _columns; y++)
            {
                _cellPositions.Add(GetPositionOnBoard(x, y));
            }
        }
    }

    private void GenerateCells()
    {
        for (int i = 0; i < _cellPositions.Count; i++)
        {
            Cell cell = Instantiate(_cellPrefab, _board.RectTransform);
            cell.transform.localScale = new Vector3(_cellScaling * _prefabCellSize.x, _cellScaling * _prefabCellSize.y, _cellScaling * _prefabCellSize.z);
            RectTransform rectTransform = cell.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = _cellPositions[i];
            CellCreated?.Invoke(cell);
        }
    }

    private void CalculateCellScaling()
    {
        float screenAspectRatio = _screenWidth / _screenHeight;

        if (screenAspectRatio > 1)
        {
            _cellScaling = _screenHeight / _screenWidth;
        }
        else
        {
            _cellScaling = _screenWidth / _screenHeight;
        }
    }
}
