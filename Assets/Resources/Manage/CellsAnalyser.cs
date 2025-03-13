using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsAnalyser : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private FieldUpdater _fieldOperator;

    private float _detectionDistance = 1;
    private WaitForSeconds _analyseDelay = new WaitForSeconds(2f);
    private List<Cell> _cells = new List<Cell>();
    private HightestGiftValueSearcher _giftValueSearcher = new HightestGiftValueSearcher();

    public Action MovedInpossible;
    public Action<int> HightestValueFound;

    public void Initial()
    {
        _cellsCreator.AllCellsCreated += AssignCellsList;
        _fieldOperator.Reformed += RunPlayingAnalyse;
        Scaler scaler = new Scaler();
        _detectionDistance = _detectionDistance / scaler.Scaling;
    }

    private void OnDisable()
    {
        _cellsCreator.AllCellsCreated -= AssignCellsList;
        _fieldOperator.Reformed -= RunPlayingAnalyse;
    }

    private void AssignCellsList(List<Cell> cells)
    {
        _cells = cells;
    }

    private IEnumerator IdentifyPossiblePlayingAfterDelay()
    {
        yield return _analyseDelay;

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
        int value = _giftValueSearcher.Search(_cells);

        HightestValueFound?.Invoke(value);
    }
}
