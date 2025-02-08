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

            List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();

            RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, _detectionDistance);
            raycastHits.Add(hitUp);
            RaycastHit2D hitDown = Physics2D.Raycast(cell.transform.position, Vector2.down, _detectionDistance);
            raycastHits.Add(hitDown);
            RaycastHit2D hitLeft = Physics2D.Raycast(cell.transform.position, Vector2.left, _detectionDistance);
            raycastHits.Add(hitLeft);
            RaycastHit2D hitRight = Physics2D.Raycast(cell.transform.position, Vector2.right, _detectionDistance);
            raycastHits.Add(hitRight);
            RaycastHit2D hitUpRight = Physics2D.Raycast(cell.transform.position, Vector2.up + Vector2.right, _detectionDistance);
            raycastHits.Add(hitUpRight);
            RaycastHit2D hitUpLeft = Physics2D.Raycast(cell.transform.position, Vector2.up + Vector2.left, _detectionDistance);
            raycastHits.Add(hitUpLeft);
            RaycastHit2D hitDownLeft = Physics2D.Raycast(cell.transform.position, Vector2.down + Vector2.left, _detectionDistance);
            raycastHits.Add(hitDownLeft);
            RaycastHit2D hitDownRight = Physics2D.Raycast(cell.transform.position, Vector2.down + Vector2.right, _detectionDistance);
            raycastHits.Add(hitDownRight);   

            foreach (var hit in raycastHits)
            {
                if (hit.collider != null && hit.collider.TryGetComponent(out Cell neighbourCell) && neighbourCell.Gift.Value == cell.Gift.Value)
                {
                    _neighboursCells.Add(neighbourCell);
                }
            }

            collider2d.enabled = true;
    }

    private void ClearNeighbourList(Cell cell)
    {
        _neighboursCells.Clear();
    }
}
