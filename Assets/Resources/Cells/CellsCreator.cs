using System;
using System.Collections.Generic;
using UnityEngine;

public class CellsCreator : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 5;
    //[SerializeField] private float _spaceCellsPercent;
    [SerializeField] private float _spaceCellsPercentWidth;
    [SerializeField] private float _spaceCellsPercentHeight;
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private float _screenSpacePercentWidth;
    [SerializeField] private float _screenSpacePercentHeight;

    private List<Vector2> _cellPositions;
    private float _screenWidth;
    private float _screenHeight;
    //private float _cellSpace;
    private float _cellSpaceWidth;
    private float _cellSpaceHeight;
    private float _screenSpaceWidth;
    private float _screenSpaceHeight;
    private float _baseScreenWidth = 1280f;
    private float _baseScreenHeight = 720;
    private float _cellScale;
    private Vector3 _baseCellSize;

    public Action<Cell> CellCreated;

    public void Initial()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        //_cellSpace = _screenWidth * _spaceCellsPercent;
        _cellSpaceWidth = _screenWidth * _spaceCellsPercentWidth;
        _cellSpaceHeight = _screenHeight * _spaceCellsPercentHeight;
        _screenSpaceWidth = _screenWidth * _screenSpacePercentWidth;
        _screenSpaceHeight = _screenHeight * _screenSpacePercentHeight;
        CalculateCellSize();
        _baseCellSize = _cellPrefab.transform.localScale;

        _cellPositions = new List<Vector2>();
        FormPositions();
        GenerateCells();
    }

    private Vector2 GetPositionOnBoard(int coordinateX, int coordinateY)
    {
        return new Vector2((-_screenWidth / 2 + _screenSpaceWidth + _cellSpaceWidth * coordinateX),
            _screenHeight / 2 - _screenSpaceHeight - _cellSpaceHeight * coordinateY);
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
            cell.transform.localScale = new Vector3(_cellScale * _baseCellSize.x, _cellScale * _baseCellSize.y, _cellScale * _baseCellSize.z);
            RectTransform rectTransform = cell.gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = _cellPositions[i];
            CellCreated?.Invoke(cell);
        }
    }

    private void CalculateCellSize()
    {
        _cellScale = Mathf.Min(_screenWidth / _baseScreenWidth);

        //_cellScale = Mathf.Min(_screenWidth / _baseScreenWidth, _screenHeight / _baseScreenHeight);
    }
}
