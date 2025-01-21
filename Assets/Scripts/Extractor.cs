using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    [SerializeField] private CellsGenerator _cellsGenerator;
    [SerializeField] private CellOperator _cellOperator; // механикf дл€ проверки соседних €чеек не работает, перепроверить

    private AttentionMonitor _attentionMonitor;
    private List<Cell> _markedCells;
    private bool _isPressed;

    public event Action TryedAddHovered;

    private void Awake()
    {
        _markedCells = new List<Cell>();
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
        _attentionMonitor.IsHovered += TryAddHovered;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsPressed += AddPressed;
        _attentionMonitor.IsRealized += ExtractCells;
        _attentionMonitor.IsHovered += TryAddHovered;
    }

    private void AddPressed(GameObject gameObject)
    {
        _markedCells.Add(gameObject.GetComponent<Cell>());
        _isPressed = true;
    }

    private void TryAddHovered(GameObject gameObject)
    {
        if (_isPressed)
        {
            if (SearchNeighbours(gameObject))
                _markedCells.Add(gameObject.GetComponent<Cell>());
        }

        TryedAddHovered?.Invoke();
    }

    private void ExtractCells()
    {
        foreach (Cell cell in _markedCells)
        {
            if (cell != null)
            {
                Destroy(cell.gameObject);
            }
        }

        _markedCells.Clear();
        _isPressed = false;
    }

    private bool SearchNeighbours(GameObject gameObject)
    {
        var cell = gameObject.GetComponent<Cell>();

        foreach (Cell neighbourCell in _cellOperator.NeighboursCells)
        {
            if (cell == neighbourCell && _markedCells[_markedCells.Count - 1].Value == cell.Value)
            {
                Debug.Log(_markedCells[_markedCells.Count - 1].Value);
                Debug.Log(neighbourCell.Value);
                return true;
            }
        }

        return false;
    }
}
