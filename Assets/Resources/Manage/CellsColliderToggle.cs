using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellsColliderToggle : MonoBehaviour
{
    [SerializeField] private CellStorage _cellStorage;
    [SerializeField] private PauseRegulator _pauseRegulator;
    [SerializeField] private TrainRegulator _trainRegulator;

    private List<Cell> _cells;

    public void Initial()
    {
        _cells = new List<Cell>();
        _cellStorage.ListFormed += AssignCellsList;
        _pauseRegulator.Paused += DisableCellsColliders;
        _pauseRegulator.Resumed += EnableCellsColliders;
        _trainRegulator.Opened += DisableCellsColliders;
        _trainRegulator.Closed += EnableCellsColliders;
    }

    private void OnDisable()
    {
        _cellStorage.ListFormed -= AssignCellsList;
        _pauseRegulator.Paused -= DisableCellsColliders;
        _pauseRegulator.Resumed -= EnableCellsColliders;
        _trainRegulator.Opened -= DisableCellsColliders;
        _trainRegulator.Closed -= EnableCellsColliders;
    }

    private void AssignCellsList(List<Cell> cells)
    {
        _cells = cells;
    }

    private void DisableCellsColliders()
    {
        foreach (var cell in _cells)
        {
            cell.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void EnableCellsColliders()
    {
        foreach (var cell in _cells)
        {
            cell.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
