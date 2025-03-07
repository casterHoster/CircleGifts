using System.Collections.Generic;
using UnityEngine;

public class NeighboursSearcher : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private float _detectionDistance = 1;

    private List<Cell> _neighboursCells;

    public List<Cell> NeighboursCells { get { return new List<Cell>(_neighboursCells); } }

    public void Initial()
    {
        _neighboursCells = new List<Cell>();
    }

    private void FindNeighbourCells(Cell cell)
    {
        var collider2d = cell.GetComponent<BoxCollider2D>();
        collider2d.enabled = false;

        CellNeighbourSearcher cellSearcher = new CellNeighbourSearcher();
        _neighboursCells = cellSearcher.Search(cell, _detectionDistance);

        collider2d.enabled = true;
    }

    private void ClearNeighbourList(Cell cell)
    {
        _neighboursCells.Clear();
    }
}
