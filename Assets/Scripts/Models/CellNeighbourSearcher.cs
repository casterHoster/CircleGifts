using System.Collections.Generic;
using UnityEngine;
using Cells;
using UnityEditor.Experimental.GraphView;

public class CellNeighbourSearcher
{
    public List<Cell> Search(Cell cell, float detectionDistance)
    {
        List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();
        List<Cell> neighboursCells = new List<Cell>();
        List<Vector2> directions = GetDirections();

        foreach(Vector2 direction in directions)
        {
            raycastHits.Add(Physics2D.Raycast(cell.transform.position, direction, detectionDistance));
        }

        //RaycastHit2D hitRight = Physics2D.Raycast(cell.transform.position, Vector2.right, detectionDistance);
        //raycastHits.Add(hitRight);
        //RaycastHit2D hitDownRight = Physics2D.Raycast(cell.transform.position, Vector2.down + Vector2.right, detectionDistance);
        //raycastHits.Add(hitDownRight);
        //RaycastHit2D hitDown = Physics2D.Raycast(cell.transform.position, Vector2.down, detectionDistance);
        //raycastHits.Add(hitDown);
        //RaycastHit2D hitDownLeft = Physics2D.Raycast(cell.transform.position, Vector2.down + Vector2.left, detectionDistance);
        //raycastHits.Add(hitDownLeft);
        //RaycastHit2D hitLeft = Physics2D.Raycast(cell.transform.position, Vector2.left, detectionDistance);
        //raycastHits.Add(hitLeft);
        //RaycastHit2D hitUpLeft = Physics2D.Raycast(cell.transform.position, Vector2.up + Vector2.left, detectionDistance);
        //raycastHits.Add(hitUpLeft);
        //RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, detectionDistance);
        //raycastHits.Add(hitUp);
        //RaycastHit2D hitUpRight = Physics2D.Raycast(cell.transform.position, Vector2.up + Vector2.right, detectionDistance);
        //raycastHits.Add(hitUpRight);

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

    private List<Vector2> GetDirections()
    {
        List<Vector2> directions = new List<Vector2>();
        directions.Add(Vector2.right);
        directions.Add(Vector2.down + Vector2.right);
        directions.Add(Vector2.down);
        directions.Add(Vector2.down + Vector2.left);
        directions.Add(Vector2.left);
        directions.Add(Vector2.up + Vector2.left);
        directions.Add(Vector2.up);
        directions.Add(Vector2.up + Vector2.right);
        return directions;
    }
}
