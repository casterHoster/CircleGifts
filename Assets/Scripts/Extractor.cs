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
    public Action<Cell, int> EndCellDifined;
    public Action<List<Cell>> Scored;
    public Action<Cell> PutOuted;
    public Action<Cell> CellAdded;

    public void Initial()
    {
        _chainedCells = new List<Cell>();
        _fieldCreator.CellCreated += SignUpMonitor;
    }

    private void OnDisable()
    {
        _fieldCreator.CellCreated -= SignUpMonitor;
        _attentionMonitor.IsPressed -= AddPressed;
        _attentionMonitor.IsRealized -= ExtractCells;
        _attentionMonitor.IsUnhovered -= SaveNeighboursByUnvovering;
        _attentionMonitor.IsHovered -= TryAddHovered;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsPressed += AddPressed;
        _attentionMonitor.IsRealized += ExtractCells;
        _attentionMonitor.IsUnhovered += SaveNeighboursByUnvovering;
        _attentionMonitor.IsHovered += TryAddHovered;
    }

    private void AddPressed(Cell cell)
    {
        if (_isPressed == false)
        {
            _chainedCells.Add(cell);
            SaveNeighboursByClicking();
            CellAdded?.Invoke(cell);
            _isPressed = true;
        }
    }

    private void TryAddHovered(Cell cell)
    {
        if (_isPressed && cell.Gift != null)
        {
            if (CheckMatchWithNeighbours(cell))
            {
                if (_chainedCells.Contains(cell))
                {
                    PutOuted?.Invoke(_chainedCells[_chainedCells.Count - 1]);
                    _chainedCells.RemoveAt(_chainedCells.Count - 1);
                }
                else
                {
                    _chainedCells.Add(cell);
                    CellAdded?.Invoke(cell);
                }
            }
        }
    }

    private void ExtractCells()
    {
        if (_chainedCells.Count > 1)
        {
            Scored?.Invoke(_chainedCells);
            Cell lastCell = _chainedCells[_chainedCells.Count - 1];
            EndCellDifined?.Invoke(lastCell, _chainedCells.Count);
            _chainedCells.Remove(lastCell);

            foreach (var cell in _chainedCells)
            {
                Extracted?.Invoke(cell);
            }
        }
        else if (_chainedCells.Count > 0)
        {
            PutOuted?.Invoke(_chainedCells[_chainedCells.Count - 1]);
        }

        _chainedCells.Clear();
        _isPressed = false;
    }

    private void SaveNeighboursByClicking()
    {
        _neighbourCells = _searcher.NeighboursCells;
    }

    private void SaveNeighboursByUnvovering(Cell cell)
    {
        if (_isPressed)
        {
            if (cell.Gift.Value == _chainedCells[_chainedCells.Count - 1].Gift.Value && _neighbourCells.Contains(cell))
                _neighbourCells = _searcher.NeighboursCells;
        }
    }

    private bool CheckMatchWithNeighbours(Cell cell)
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
}
