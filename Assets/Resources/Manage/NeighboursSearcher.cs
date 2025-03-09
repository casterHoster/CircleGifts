using System.Collections.Generic;
using UnityEngine;

public class NeighboursSearcher : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private float _detectionDistance = 1;

    private AttentionMonitor _attentionMonitor;
    private List<Cell> _neighboursCells;

    public List<Cell> NeighboursCells { get { return new List<Cell>(_neighboursCells); } }

    public virtual void Initial()
    {
        _neighboursCells = new List<Cell>();
        _cellsCreator.AllCellsCreated += SignUpMonitors;
    }

    public void FindNeighbourCells(Cell cell)
    {
        var collider2d = cell.GetComponent<BoxCollider2D>();
        collider2d.enabled = false;

        CellNeighbourSearcher cellSearcher = new CellNeighbourSearcher();
        _neighboursCells = cellSearcher.Search(cell, _detectionDistance);

        collider2d.enabled = true;
    }

    private void OnDisable()
    {
        _cellsCreator.AllCellsCreated -= SignUpMonitors;
        _attentionMonitor.IsHovered -= FindNeighbourCells;
        _attentionMonitor.IsUnhovered -= ClearNeighbourList;
    }

    private void SignUpMonitors(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
            _attentionMonitor.IsHovered += FindNeighbourCells;
            _attentionMonitor.IsUnhovered += ClearNeighbourList;
        }
    }

    private void ClearNeighbourList(Cell cell)
    {
        _neighboursCells.Clear();
    }
}
