using System;
using System.Collections.Generic;
using UnityEngine;

public class CellStorage : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellCreator;

    private List<Cell> _cells = new List<Cell>();

    public Action<List<Cell>> ListFormed;

    public void Initial()
    {
        _cellCreator.CellCreated += AddCell;
        _cellCreator.AllCellsCreated += GiveListToHandling;
    }

    private void OnDisable()
    {
        _cellCreator.CellCreated -= AddCell;
        _cellCreator.AllCellsCreated -= GiveListToHandling;
    }

    private void AddCell(Cell cell)
    {
        _cells.Add(cell);
    }

    private void GiveListToHandling()
    {
        ListFormed?.Invoke(_cells);
    }
}
