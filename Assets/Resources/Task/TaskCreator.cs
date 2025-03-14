using System.Collections.Generic;

public class TaskCreator
{
    private int _targetValue;
    private int _targetGiftsCount;
    private int _targetMovesCount;
    private float _movesCoefficient = 2;
    private int _minValue = 2;
    private int _maxValue = 1024;
    private int _minGiftsValueSpread = -2;
    private int _maxGiftsValueSpread = 2;
    private int _minGiftsCount = 5;
    private int _maxGiftsCount = 20;
    private List<Cell> _cells;

    public TaskCreator(List<Cell> cells)
    {
        _cells = cells;
    }

    public Task GetTask()
    {
        FormTargetValue();
        FormGiftCount();
        FormMovesCount();
        Task task = new Task(_targetValue, _targetGiftsCount, _targetMovesCount);
        return task;
    }

    private void FormTargetValue()
    {
        _targetValue = new HightestGiftValueSearcher().Search(_cells);
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

    private int GetRandomSpread()
    {
        return UnityEngine.Random.Range(_minGiftsValueSpread, _maxGiftsValueSpread + 1);
    }
}
