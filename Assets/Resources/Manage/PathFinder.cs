using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;

    private List<Cell> _cells = new List<Cell>();


    private void OnEnable()
    {
        _cellsCreator.CellCreated += AddCellList;
    }

    private void AddCellList(Cell cell)
    {
        _cells.Add(cell);
    }

    private void FindPath()
    {

    }

    private void FindNeighbourCells(Cell cell)
    {
        var collider2d = cell.GetComponent<BoxCollider2D>();
        collider2d.enabled = false;

        CellNeighbourSearcher cellSearcher = new CellNeighbourSearcher();


        collider2d.enabled = true;
    }
}
