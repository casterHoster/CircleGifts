using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellsColliderToggle : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private PauseRegulator _pauseRegulator;
    [SerializeField] private TrainRegulator _trainRegulator;

    private List<Cell> _cells;

    public void Initial()
    {
        _cells = new List<Cell>();
        _cellsCreator.CellCreated += AddCell;
        _pauseRegulator.Paused += DisableCellsColliders;
        _pauseRegulator.Resumed += EnableCellsColliders;
        _trainRegulator.Opened += DisableCellsColliders;
        _trainRegulator.Closed += EnableCellsColliders;
    }

    private void OnDisable()
    {
        _cellsCreator.CellCreated -= AddCell;
        _pauseRegulator.Paused -= DisableCellsColliders;
        _pauseRegulator.Resumed -= EnableCellsColliders;
        _trainRegulator.Opened -= DisableCellsColliders;
        _trainRegulator.Closed -= EnableCellsColliders;
    }

    private void AddCell(Cell cell)
    {
        _cells.Add(cell);
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
