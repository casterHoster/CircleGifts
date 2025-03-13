using System.Collections.Generic;
using UnityEngine;

public class TaskCreator : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private CellsAnalyser _cellsAnalyser;

    private int _targetValue;
    private int _targetGiftsCount;
    private int _targetMovesCount;
    private float _movesCoefficient = 1.5f;
    private int _minValue = 2;
    private int _maxValue = 1024;
    private int _minGiftsValueSpread = -2;
    private int _maxGiftsValueSpread = 2;
    private int _minGiftsCount = 10;
    private int _maxGiftsCount = 30;
    private List<Cell> _cells = new List<Cell>();
    private HightestGiftValueSearcher _hightestGiftValueSearcher = new HightestGiftValueSearcher();

    private void CreateTask()
    {
        FormTargetValue();
        FormGiftCount();
        FormMovesCount();
        Task task = new Task(_targetValue, _targetGiftsCount, _targetMovesCount);
    }

    private void FormTargetValue()
    {
        _targetValue = _hightestGiftValueSearcher.Search(_cells);
        int step = GetRandomSpread();

        if (step > 0)
        {
            for (int i = 0; i < step; i++)
            {
                _targetValue *= 2;
            }
        }

        else if (step < 0)
        {
            for (int i = 0; i > step; i--)
            {
                _targetValue /= 2;
            }
        }

        if (_targetValue > _maxValue)
            _targetValue = _maxValue;

        if (_targetValue < _minValue) 
            _targetValue = _minValue;
    }

    private void FormGiftCount()
    {
        _targetGiftsCount = UnityEngine.Random.Range(_minGiftsCount, _maxGiftsCount + 1);
    }

    private void FormMovesCount()
    {
        _targetMovesCount = (int)(_targetGiftsCount * _movesCoefficient);
    }

    private void AssignCellsList(List<Cell> cells)
    {
        _cells = cells;
    }

    private int GetRandomSpread()
    {
        return UnityEngine.Random.Range(_minGiftsValueSpread, _maxGiftsValueSpread + 1);
    }
}
