using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsAnalyser : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private FieldUpdater _fieldOperator;
    [SerializeField] private float _detectionDistance = 1;

    private WaitForSeconds _analyseDalay = new WaitForSeconds(2f);
    private List<Cell> _cells = new List<Cell>();

    public Action MovedInpossible;
    public Action<int> HightestValueFound;

    public void Initial()
    {
        _cellsCreator.CellCreated += AddCell;
        _fieldOperator.Reformed += RunPlayingAnalyse;
    }

    private void OnDisable()
    {
        _cellsCreator.CellCreated -= AddCell;
        _fieldOperator.Reformed -= RunPlayingAnalyse;
    }

    private void AddCell(Cell cell)
    {
        _cells.Add(cell);
    }

    private IEnumerator IdentifyPossiblePlayingAfterDelay()
    {
        yield return _analyseDalay;

        if (TryFindPerspectiveCells() == false)
        {
            MovedInpossible?.Invoke();
        }
    }

    private void RunPlayingAnalyse()
    {
        StartCoroutine(IdentifyPossiblePlayingAfterDelay());
        FindHighestGiftValue();
    }

    private bool TryFindPerspectiveCells()
    {
        CellNeighbourSearcher neighbourSearcher = new CellNeighbourSearcher();
        List<Cell> neighbourCells = new List<Cell>();

        foreach (var cell in _cells)
        {
            var collider2d = cell.GetComponent<BoxCollider2D>();
            collider2d.enabled = false;
            neighbourCells = neighbourSearcher.Search(cell, _detectionDistance);
            collider2d.enabled = true;

            foreach (var neighbourCell in neighbourCells)
            {
                if (neighbourCell.Gift.Value == cell.Gift.Value)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void FindHighestGiftValue()
    {
        List<Cell> cellsWithValues = new List<Cell>();

        foreach(var cell in _cells)
        {
            if (cell.Gift != null)
            {
                cellsWithValues.Add(cell);
            }
        }

        int value = cellsWithValues[0].Gift.Value;

        for (int i = 1; i < cellsWithValues.Count; i++)
        {
            if (cellsWithValues[i].Gift.Value > value)
            {
                value = cellsWithValues[i].Gift.Value;
            }
        }

        HightestValueFound?.Invoke(value);
    }
}
