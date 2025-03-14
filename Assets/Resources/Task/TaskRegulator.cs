using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskRegulator : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private Extractor _extractor;

    private List<Cell> _cells;
    private TaskCreator taskCreator;
    private Task _currentTask;
    private int _taskGiftValue;
    private int _taskGiftsCount;
    private int _taskMoves;

    public Action<Task> TaskSet;
    public Action<int, int> Compared;
    public Action MovesEnded;

    public void Initial()
    {
        _cellsCreator.AllCellsCreated += AssignCellsList;
        _extractor.ListIsDefined += CompareTargetValues;
    }

    private void AssignCellsList(List<Cell> cells)
    {
        _cells = cells;
        taskCreator = new TaskCreator(_cells);
        SetNewTask();
    }

    private void SetNewTask()
    {
        _currentTask = taskCreator.GetTask();
        _taskGiftsCount = _currentTask.GiftsCount;
        _taskMoves = _currentTask.Moves;

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
}
