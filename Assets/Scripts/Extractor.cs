using System;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    [SerializeField] private CellsGenerator _cellsGenerator;
    [SerializeField] private NeighboursSearcher _searcher;

    private AttentionMonitor _attentionMonitor;
    private List<Cell> _markedGifts;
    private List<Cell> _neighboursCells;
    private bool _isPressed;

    public event Action TryedAddHovered;

    private void Awake()
    {
        _markedGifts = new List<Cell>();
    }

    private void OnEnable()
    {
        _cellsGenerator.Instantiated += SignUpMonitor;
    }

    private void OnDisable()
    {
        _cellsGenerator.Instantiated -= SignUpMonitor;
        _attentionMonitor.IsPressed -= AddPressed;
        _attentionMonitor.IsRealized -= ExtractCells;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsPressed += AddPressed;
        _attentionMonitor.IsRealized += ExtractCells;
        _attentionMonitor.IsUnhovered += SaveNeighbours;
        _attentionMonitor.IsHovered += TryAddHovered;
    }

    private void AddPressed(Cell cell)
    {
        if (_isPressed == false)
        {
            _markedGifts.Add(cell);
            _isPressed = true;
        }
    }

    private void TryAddHovered(Cell cell)
    {
        if (_isPressed)
        {
            if (CheckMatch(cell))
                _markedGifts.Add(cell);
        }

        TryedAddHovered?.Invoke();
    }

    private void ExtractCells()
    {
        if (_markedGifts.Count > 1)
        {
            foreach (var cell in _markedGifts)
            {
                if (cell != null)
                {
                    Destroy(cell.gameObject);
                }
            }
        }

        _markedGifts.Clear();
        _isPressed = false;
    }

    private void SaveNeighbours(Cell cell)
    {
        if (!_isPressed)
            _neighboursCells = _searcher.NeighboursCells;
        else
        {
            if (cell.Value == _markedGifts[_markedGifts.Count - 1].Value)
                _neighboursCells = _searcher.NeighboursCells;
        }
            
    }

    private bool CheckMatch(Cell cell)
    {
        foreach (Cell neighbourCell in _neighboursCells)
        {
            if (neighbourCell != null)
            {
                if (_markedGifts[_markedGifts.Count - 1].Value == cell.Value && cell.gameObject == neighbourCell.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
