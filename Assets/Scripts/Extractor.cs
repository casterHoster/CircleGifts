using System;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    [SerializeField] private CellsCreator _fieldCreator;
    [SerializeField] private NeighboursSearcher _searcher;

    private AttentionMonitor _attentionMonitor;
    private List<Cell> _chainedCells;
    private List<Cell> _neighbourCells;
    private bool _isPressed;

    public Action<Cell> Extracted;
    public Action<Cell> EndCellDifined;
    public Action<List<Cell>> Processed;

    public void Initial()
    {
        _chainedCells = new List<Cell>();
        _fieldCreator.CellCreated += SignUpMonitor;
    }

    private void OnDisable()
    {
        _fieldCreator.CellCreated -= SignUpMonitor;
        _attentionMonitor.IsPressed -= AddPressed;
        _attentionMonitor.IsRealized -= ExtractCell;
        _attentionMonitor.IsUnhovered -= SaveNeighbours;
        _attentionMonitor.IsHovered -= TryAddHovered;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsPressed += AddPressed;
        _attentionMonitor.IsRealized += ExtractCell;
        _attentionMonitor.IsUnhovered += SaveNeighbours;
        _attentionMonitor.IsHovered += TryAddHovered;
    }

    private void AddPressed(Cell cell)
    {
        if (_isPressed == false)
        {
            _chainedCells.Add(cell);
            _isPressed = true;
        }
    }

    private void TryAddHovered(Cell cell)
    {
        if (_isPressed && cell.Gift != null)
        {
            if (CheckMatch(cell) && !_chainedCells.Contains(cell))
                _chainedCells.Add(cell);

            RemoveNotInChain(cell);
        }
    }

    private void ExtractCell()
    {
        if (_chainedCells.Count > 1)
        {
            Processed?.Invoke(_chainedCells);

            Cell lastCell = _chainedCells[_chainedCells.Count - 1];

            EndCellDifined?.Invoke(lastCell);

            _chainedCells.Remove(lastCell);

            foreach (var cell in _chainedCells)
            {
                Extracted?.Invoke(cell);
            }
        }

        _chainedCells.Clear();
        _isPressed = false;
    }

    private void SaveNeighbours(Cell cell)
    {
        if (!_isPressed)
            _neighbourCells = _searcher.NeighboursCells;
        else
        {
            if (cell.Gift.Value == _chainedCells[_chainedCells.Count - 1].Gift.Value)
                _neighbourCells = _searcher.NeighboursCells;
        }

    }

    private bool CheckMatch(Cell cell)
    {
        foreach (Cell neighbourCell in _neighbourCells)
        {
            if (neighbourCell != null)
            {
                if (_chainedCells[_chainedCells.Count - 1].Gift.Value == cell.Gift.Value && cell.gameObject == neighbourCell.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void RemoveNotInChain(Cell cell)
    {
        if (_chainedCells.Count >= 2 && cell == _chainedCells[_chainedCells.Count - 2])
        {
            _chainedCells.RemoveAt(_chainedCells.Count - 2);
        }
    }
}
