using System.Collections.Generic;
using Cells;
using ModelPattern;

namespace Tasks
{
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
        private int _maxGiftsCount = 15;
        private List<Cell> _cells;

        public TaskCreator(List<Cell> cells)
        {
            _cells = cells;
        }

        public TargetTask GetTask()
        {
            FormTargetValue();
            FormGiftCount();
            FormMovesCount();
            TargetTask task = new TargetTask(_targetValue, _targetGiftsCount, _targetMovesCount);
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
            else if (step <= 0)
            {
                for (int i = 0; i > step; i--)
                {
                    _targetValue /= 2;
                }
            }

            if (_targetValue > _maxValue || _targetValue < _minValue)
            {
                FormTargetValue();
            }
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
}
