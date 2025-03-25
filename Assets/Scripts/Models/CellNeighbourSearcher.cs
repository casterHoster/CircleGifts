using System.Collections.Generic;
using UnityEngine;
using Cells;

public class CellNeighbourSearcher
{
    public List<Cell> Search(Cell cell, float detectionDistance)
    {
        List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();
        List<Cell> neighboursCells = new List<Cell>();

        RaycastHit2D hitRight = Physics2D.Raycast(cell.transform.position, Vector2.right, detectionDistance);
        raycastHits.Add(hitRight);
        RaycastHit2D hitDownRight = Physics2D.Raycast(cell.transform.position, Vector2.down + Vector2.right, detectionDistance);
        raycastHits.Add(hitDownRight);
        RaycastHit2D hitDown = Physics2D.Raycast(cell.transform.position, Vector2.down, detectionDistance);
        raycastHits.Add(hitDown);
        RaycastHit2D hitDownLeft = Physics2D.Raycast(cell.transform.position, Vector2.down + Vector2.left, detectionDistance);
        raycastHits.Add(hitDownLeft);
        RaycastHit2D hitLeft = Physics2D.Raycast(cell.transform.position, Vector2.left, detectionDistance);
        raycastHits.Add(hitLeft);
        RaycastHit2D hitUpLeft = Physics2D.Raycast(cell.transform.position, Vector2.up + Vector2.left, detectionDistance);
        raycastHits.Add(hitUpLeft);
        RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, detectionDistance);
        raycastHits.Add(hitUp);
        RaycastHit2D hitUpRight = Physics2D.Raycast(cell.transform.position, Vector2.up + Vector2.right, detectionDistance);
        raycastHits.Add(hitUpRight);

        foreach (var hit in raycastHits)
        {
            if (hit.collider != null && hit.collider.TryGetComponent(out Cell neighbourCell)
            && neighbourCell.Gift != null && cell.Gift != null && neighbourCell.Gift.Value == cell.Gift.Value)
            {
                neighboursCells.Add(neighbourCell);
            }
        }

        return neighboursCells;
    }
}
