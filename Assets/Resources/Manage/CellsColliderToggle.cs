using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellsColliderToggle : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private PauseRegulator _pauseRegulator;
    [SerializeField] private GameFinisher _gameFinisher;

    private List<Cell> _cells;

    public void Initial()
    {
        _cellsCreator.AllCellsCreated += AssignCellsList;
        _pauseRegulator.Paused += DisableCellsColliders;
        _pauseRegulator.Resumed += EnableCellsColliders;
        _gameFinisher.GameIsOver += DisableCellsColliders;
        _gameFinisher.GameIsContinued += EnableCellsColliders;
    }

    private void OnDisable()
    {
        _cellsCreator.AllCellsCreated -= AssignCellsList;
        _pauseRegulator.Paused -= DisableCellsColliders;
        _pauseRegulator.Resumed -= EnableCellsColliders;
        _gameFinisher.GameIsOver -= DisableCellsColliders;
        _gameFinisher.GameIsContinued -= EnableCellsColliders;
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
