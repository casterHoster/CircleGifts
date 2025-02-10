using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private FieldCreator _fieldCreator;

    private float _detectionDistance = 1;
    private float _speed = 2;
    private float threshold = 0.1f;
    private Queue<Cell> _cells;
    private bool _isProcessed;
    private WaitForSeconds _cooldown = new WaitForSeconds(0.05f);

    public void Initial()
    {
        _cells = new Queue<Cell>();
        StartCoroutine(CheckQueueCells());
        _fieldCreator.CellCreated += SignUpClearing;
    }

    private void SignUpClearing(Cell cell)
    {
        cell.Cleared += AddCellQueue;
    }

    private void AddCellQueue(Cell cell)
    {
        _cells.Enqueue(cell);
    }

    private IEnumerator CheckQueueCells()
    {
        while (enabled)
        {
            if (_cells.Count > 0 && _isProcessed == false)
            {
                _isProcessed = true;
                RelocateGift();
            }

            yield return _cooldown;
        }
    }

    private void RelocateGift()
    {
        Cell cell = _cells.Dequeue();
        var collider2d = cell.GetComponent<BoxCollider2D>();
        collider2d.enabled = false;

        RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, _detectionDistance);

        if (hitUp.collider != null && hitUp.collider.TryGetComponent(out Cell upCell) && !_cells.Contains(upCell))
        {
            StartCoroutine(Move(upCell.Gift, cell));
            cell.Fill(upCell.Gift);
            upCell.Clear();
        }
        else 
        {
            _cells.Enqueue(cell);
        }

        collider2d.enabled = true;
        _isProcessed = false;
    }

    private IEnumerator Move(Gift gift, Cell cell)
    {
        Vector3 targetPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);

        while (Vector3.Distance(gift.transform.position, cell.transform.position) > threshold)
        {
            gift.transform.position = Vector3.MoveTowards(gift.transform.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }

        gift.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
    }
}
