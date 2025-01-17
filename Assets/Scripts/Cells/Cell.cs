using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent (typeof(AttentionMonitor))]
public abstract class Cell : MonoBehaviour
{
    private List<Cell> NeighboursCells = new List<Cell>();
    private Vector3 _previousPosition;
    private float _detectionDistance = 1;

    public int Value { get; protected set; }
    public RectTransform RectTransform { get; private set; }

    protected virtual void Awake()
    {
        _previousPosition = transform.position;
        RectTransform = GetComponent<RectTransform>();
        FindNeighboursCells();
    }

    private void Update()
    {
        if (_previousPosition != transform.position)
        {
            NeighboursCells.Clear();
            FindNeighboursCells();
            _previousPosition = transform.position;
        }

    }

    private void FindNeighboursCells()
    {
        List<RaycastHit2D[]> raycastHits = new List<RaycastHit2D[]>();

        RaycastHit2D[] hitUp = Physics2D.RaycastAll(transform.position, Vector2.up, _detectionDistance);
        raycastHits.Add(hitUp);
        RaycastHit2D[] hitDown = Physics2D.RaycastAll(transform.position, Vector2.down, _detectionDistance);
        raycastHits.Add(hitDown);
        RaycastHit2D[] hitLeft = Physics2D.RaycastAll(transform.position, Vector2.left, _detectionDistance);
        raycastHits.Add(hitLeft);
        RaycastHit2D[] hitRight = Physics2D.RaycastAll(transform.position, Vector2.right, _detectionDistance);
        raycastHits.Add(hitRight);
        RaycastHit2D[] hitUpRight = Physics2D.RaycastAll(transform.position, Vector2.up + Vector2.right, _detectionDistance);
        raycastHits.Add(hitUpRight);
        RaycastHit2D[] hitUpLeft = Physics2D.RaycastAll(transform.position, Vector2.up + Vector2.left, _detectionDistance);
        raycastHits.Add(hitUpLeft);
        RaycastHit2D[] hitDownLeft = Physics2D.RaycastAll(transform.position, Vector2.down + Vector2.left, _detectionDistance);
        raycastHits.Add(hitDownLeft);
        RaycastHit2D[] hitDownRight = Physics2D.RaycastAll(transform.position, Vector2.down + Vector2.right, _detectionDistance);
        raycastHits.Add(hitDownRight);

        foreach (var hits in raycastHits)
        {
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject != gameObject)
                {
                    hit.collider.TryGetComponent(out Cell cell);
                    NeighboursCells.Add(cell);
                }
            }
        }
    }
}