using System;
using System.Collections.Generic;
using UnityEngine;
using Cells;
using Gameplay;
using Monetization;

namespace Tasks
{
    public class TaskRegulator : MonoBehaviour
    {
        [SerializeField] private CellsCreator _cellsCreator;
        [SerializeField] private Extractor _extractor;
        [SerializeField] private Reward _reward;

        private List<Cell> _cells;
        private TaskCreator _taskCreator;
        private TargetTask _currentTask;
        private int _taskGiftsCount;
        private int _taskMoves;
        private int _initialMovesCount;

        public event Action<TargetTask> TaskSet;
        public event Action<int, int> Compared;
        public event Action MovesEnded;

        public void Initial()
        {
            _cellsCreator.AllCellsCreated += AssignCellsList;
            _extractor.ListIsDefined += CompareTargetValues;
            _reward.Rewarded += ResetMoves;
        }

        private void OnDisable()
        {
            _cellsCreator.AllCellsCreated -= AssignCellsList;
            _extractor.ListIsDefined -= CompareTargetValues;
            _reward.Rewarded -= ResetMoves;
        }

        private void AssignCellsList(List<Cell> cells)
        {
            _cells = cells;
            _taskCreator = new TaskCreator(_cells);
            SetNewTask();
        }

        private void SetNewTask()
        {
            _currentTask = _taskCreator.GetTask();
            _taskGiftsCount = _currentTask.GiftsCount;
            _taskMoves = _currentTask.Moves;
            _initialMovesCount = _currentTask.Moves;

            TaskSet?.Invoke(_currentTask);
        }

        private void CompareTargetValues(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                if (cell.Gift.Value == _currentTask.GiftValue)
                {
                    _taskGiftsCount--;
                }
            }

            _taskMoves--;
            Compared?.Invoke(_taskGiftsCount, _taskMoves);

            if (_taskGiftsCount <= 0)
            {
                SetNewTask();
            }

            if (_taskMoves == 0)
            {
                MovesEnded?.Invoke();
            }
        }

        private void ResetMoves()
        {
            _taskMoves = _initialMovesCount;
            Compared?.Invoke(_taskGiftsCount, _taskMoves);
        }
    }
}
