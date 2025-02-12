using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsAnalyser : MonoBehaviour
{
    [SerializeField] NeighboursSearcher _neighboursSearcher;
    [SerializeField] FieldCreator _fieldCreator;

    private List<Cell> _cells;

    private void OnEnable()
    {
        _fieldCreator.CellCreated += AddCell;
    }

    private void AddCell(Cell cell)
    {
        _cells.Add(cell);
    }
}
