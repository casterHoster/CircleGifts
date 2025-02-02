using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBuilder : MonoBehaviour
{
    [SerializeField] private CellsGenerator _cellsGenerator;
    [SerializeField] private NeighboursSearcher _searcher;

    private AttentionMonitor _attentionMonitor;
    private List<GameObject> _chainedGifts;
    private List<Cell> _neighboursCells;
    private bool _isPressed;

    public event Action TryedAddHovered;

    private void Awake()
    {
        _chainedGifts = new List<GameObject>();
        _cellsGenerator.Instantiated += SignUpMonitor;
    }

    private void OnDisable()
    {
        _cellsGenerator.Instantiated -= SignUpMonitor;
        _attentionMonitor.IsPressed -= AddPressed;
        _attentionMonitor.IsUnhovered -= SaveNeighbours;
        _attentionMonitor.IsHovered -= TryAddHovered;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsPressed += AddPressed;
        _attentionMonitor.IsUnhovered += SaveNeighbours;
        _attentionMonitor.IsHovered += TryAddHovered;
    }

    private void AddPressed(GameObject gift)
    {
        if (_isPressed == false)
        {
            _chainedGifts.Add(gift);
            _isPressed = true;
        }
    }

    private void TryAddHovered(GameObject gift)
    {
        if (_isPressed)
        {
            if (CheckMatch(gift))
                _chainedGifts.Add(gift);
        }

        TryedAddHovered?.Invoke();
    }

    private void SaveNeighbours(GameObject gift)
    {
        if (!_isPressed)
            _neighboursCells = _searcher.NeighboursCells;
        else
        {
            gift.TryGetComponent(out Cell cell);

            if (cell.Value == _chainedGifts[_chainedGifts.Count - 1].GetComponent<Cell>().Value)
                _neighboursCells = _searcher.NeighboursCells;
        }

    }

    private bool CheckMatch(GameObject gift)
    {
        foreach (Cell neighbourCell in _neighboursCells)
        {
            if (neighbourCell != null)
            {
                if (_chainedGifts[_chainedGifts.Count - 1].GetComponent<Cell>().Value == gift.GetComponent<Cell>().Value && gift == neighbourCell.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
