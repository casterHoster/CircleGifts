using System.Collections.Generic;
using UnityEngine;

public class NeighboursSearcher : MonoBehaviour
{
    [SerializeField] protected CellStorage _cellStorage;
    [SerializeField] protected float _detectionDistance = 1;

    protected List<Cell> _neighboursCells;

    public List<Cell> NeighboursCells { get { return new List<Cell>(_neighboursCells); } }

    public virtual void Initial()
    {
        _neighboursCells = new List<Cell>();
    }

    public void FindNeighbourCells(Cell cell)
    {
        var collider2d = cell.GetComponent<BoxCollider2D>();
        collider2d.enabled = false;

        CellNeighbourSearcher cellSearcher = new CellNeighbourSearcher();
        _neighboursCells = cellSearcher.Search(cell, _detectionDistance);

        collider2d.enabled = true;
    }
}
