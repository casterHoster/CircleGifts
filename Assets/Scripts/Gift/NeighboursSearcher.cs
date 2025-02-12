using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NeighboursSearcher : MonoBehaviour
{
    [SerializeField] private FieldCreator _fieldCreator;

    private AttentionMonitor _attentionMonitor;
    private List<Cell> _neighboursCells;
    private float _detectionDistance = 1;

    public List<Cell> NeighboursCells { get { return new List<Cell>(_neighboursCells); } }

    public void Initial()
    {
        _neighboursCells = new List<Cell>();
        _fieldCreator.CellCreated += SignUpMonitor;
    }


    private void OnDisable()
    {
        _attentionMonitor.IsHovered -= FindNeighbourCells;
        _fieldCreator.CellCreated -= SignUpMonitor;
        _attentionMonitor.IsUnhovered -= ClearNeighbourList;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsHovered += FindNeighbourCells;
        _attentionMonitor.IsUnhovered += ClearNeighbourList;
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
