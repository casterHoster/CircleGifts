using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public abstract class Cell : MonoBehaviour
{
    public List<Cell> NeighboursCells = new List<Cell>();

    private Vector3 previousPosition;

    public int Value {  get; protected set; }
    public RectTransform RectTransform { get; private set; }

    protected virtual void Awake()
    {
        previousPosition = transform.position;
        RectTransform = GetComponent<RectTransform>();
        FindNeighboursCells();
    }

    private void Update()
    {
        if (previousPosition != transform.position)
        {
            FindNeighboursCells();
        }

        previousPosition = transform.position;
    }

    private void FindNeighboursCells()
    {
        List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);
        raycastHits.Add(hitUp);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);
        raycastHits.Add(hitDown);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left);
        raycastHits.Add(hitLeft);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right);
        raycastHits.Add(hitRight);
        RaycastHit2D hitUpRight = Physics2D.Raycast(transform.position, Vector2.up + Vector2.right);
        raycastHits.Add(hitUpRight);
        RaycastHit2D hitUpLeft = Physics2D.Raycast(transform.position, Vector2.up + Vector2.left);
        raycastHits.Add(hitUpLeft);
        RaycastHit2D hitDownLeft = Physics2D.Raycast(transform.position, Vector2.down + Vector2.left);
        raycastHits.Add(hitDownLeft);
        RaycastHit2D hitDownRight = Physics2D.Raycast(transform.position, Vector2.down + Vector2.right);
        raycastHits.Add(hitDownRight);

        foreach (var hit in raycastHits)
        {
            if (hit)
            {
                hit.collider.TryGetComponent(out Cell cell);
                NeighboursCells.Add(cell);
            }
        }
    }
}