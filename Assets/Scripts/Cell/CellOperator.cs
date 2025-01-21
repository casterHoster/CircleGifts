using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellOperator : MonoBehaviour
{
    [SerializeField] private CellsGenerator _cellsGenerator;
    [SerializeField] private Extractor _extractor;

    private AttentionMonitor _attentionMonitor;
    private List<Cell> _neighboursCells = new List<Cell>();
    private float _detectionDistance = 1;
    private bool _isCursorStayOnCell;

    public List<Cell> NeighboursCells { get { return new List<Cell>(_neighboursCells); } }

    private void OnEnable()
    {
        _cellsGenerator.Instantiated += SignUpMonitor;
        _extractor.TryedAddHovered += ClearNeighbourList;
    }


    private void OnDisable()
    {
        _attentionMonitor.IsHovered -= FindNeighboursCells;
        _cellsGenerator.Instantiated -= SignUpMonitor;
    }

    private void SignUpMonitor(Cell cell)
    {
        _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsHovered += FindNeighboursCells;
        _attentionMonitor.IsUnhovered += ResetCursorStayStatus;
    }

    private void FindNeighboursCells(GameObject gameObject)
    {
        if (_isCursorStayOnCell == false)
        {
            var collider2d = gameObject.GetComponent<BoxCollider2D>();
            collider2d.enabled = false;

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
                        _neighboursCells.Add(cell);
                    }
                }
            }

            collider2d.enabled = true;
        }

        _isCursorStayOnCell = true;

           // вот здесь нужно передать событие экстрактору с соседними €чейками, чтобы экстрактор сразу у себ€ их сохранил и передал дл€ сохранени€ и определени€,
        //будет следующий игровой объект, которого коснетс€ игрок одним из этих соседей
    }

    private void ClearNeighbourList()
    {
        _neighboursCells.Clear();
    }

    private void ResetCursorStayStatus()
    {
        _isCursorStayOnCell = false;
    }
}
