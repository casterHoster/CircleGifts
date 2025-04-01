using System.Collections.Generic;
using UnityEngine;
using Cells;

namespace ModelPattern
{
    public class CellNeighbourSearcher
    {
        public List<Cell> Search(Cell cell, float detectionDistance)
        {
            List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();
            List<Cell> neighboursCells = new List<Cell>();
            List<Vector2> directions = GetDirections();

            foreach (Vector2 direction in directions)
            {
                raycastHits.Add(Physics2D.Raycast(cell.transform.position, direction, detectionDistance));
            }

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
}
