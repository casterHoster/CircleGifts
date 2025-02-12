using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsAnalyser : MonoBehaviour
{
    [SerializeField] NeighboursSearcher _neighboursSearcher;
    [SerializeField] FieldCreator _fieldCreator;

    private float _detectionDistance = 1;
    private List<Cell> _cells;

    private void OnEnable()
    {
        _fieldCreator.CellCreated += AddCell;
    }

    private void AddCell(Cell cell)
    {
        _cells.Add(cell);
    }

    private bool TryFindPerspectiveCells()
    {
        CellNeighbourSearcher neighbourSearcher = new CellNeighbourSearcher();
        List<Cell> neighbourCells = new List<Cell>();

        foreach (var cell in _cells)
        {
            neighbourCells = neighbourSearcher.Search(cell, _detectionDistance);

            foreach (var neighbour in neighbourCells)
            {
                if (cell.Gift.Value == neighbour.Gift.Value)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
