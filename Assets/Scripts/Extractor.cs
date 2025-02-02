using System;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    [SerializeField] private CellsGenerator _cellsGenerator;
    [SerializeField] private NeighboursSearcher _searcher;

    private AttentionMonitor _attentionMonitor;
    private List<GameObject> _markedGifts;
    private List<Cell> _neighboursCells;
    private bool _isPressed;

    public event Action TryedAddHovered;

    private void Awake()
    {
        _markedGifts = new List<GameObject>();
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
            _markedGifts.Add(cell.gameObject);
            _isPressed = true;
        }
    }

    private void TryAddHovered(Cell gift)
    {
        if (_isPressed)
        {
            if (CheckMatch(gift.gameObject))
                _markedGifts.Add(gift.gameObject);
        }

        TryedAddHovered?.Invoke();
    }

    private void ExtractCells()
    {
        if (_markedGifts.Count > 1)
        {
            foreach (var gift in _markedGifts)
            {
                if (gift != null)
                {
                    Destroy(gift);
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
            if (cell.Value == _markedGifts[_markedGifts.Count - 1].GetComponent<Cell>().Value)
                _neighboursCells = _searcher.NeighboursCells;
        }
            
    }

    private bool CheckMatch(GameObject gift)
    {
        foreach (Cell neighbourCell in _neighboursCells)
        {
            if (neighbourCell != null)
            {
                if (_markedGifts[_markedGifts.Count - 1].GetComponent<Cell>().Value == gift.GetComponent<Cell>().Value && gift == neighbourCell.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
