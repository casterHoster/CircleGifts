using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOperator : MonoBehaviour
{
    [SerializeField] private FieldCreator _fieldCreator;
    [SerializeField] private GiftsPool _giftsPool;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _generateDelay;

    private float _detectionDistance = 1;
    private float threshold = 0.1f;
    private Queue<Cell> _cells;
    private WaitForSeconds _cooldownChecking = new WaitForSeconds(0.05f);

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
            if (_cells.Count > 0)
            {
                Reform();
            }

            yield return _cooldownChecking;
        }
    }

    private void Reform()
    {
        Cell cell = _cells.Dequeue();
        var collider2d = cell.GetComponent<BoxCollider2D>();
        collider2d.enabled = false;

        RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, _detectionDistance);

        if (hitUp.collider != null)
        {
            if (hitUp.collider.TryGetComponent(out Cell upCell) && !_cells.Contains(upCell) && upCell.Gift != null)
            {
                cell.Fill(upCell.Gift);
                upCell.Clear();
                StartCoroutine(Move(cell.Gift, cell));
            }
            else
            {
                _cells.Enqueue(cell);
            }
        }
        else
        {
            if (cell.HasGift == false)
                StartCoroutine(GenerateGiftWithDelay(cell));
            else
                _cells.Enqueue(cell);
        }

        collider2d.enabled = true;
    }

    private IEnumerator GenerateGiftWithDelay(Cell cell)
    {
        yield return new WaitForSeconds(_generateDelay);

        _giftsPool.Generate(cell);
        cell.Gift.transform.position = 
            new Vector3(cell.Gift.gameObject.transform.position.x, cell.Gift.gameObject.transform.position.y + 1, cell.Gift.gameObject.transform.position.z);
        StartCoroutine(Move(cell.Gift, cell));
    }

    private IEnumerator Move(Gift gift, Cell cell)
    {
        Vector3 targetPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);

        while (Vector3.Distance(gift.transform.position, cell.transform.position) > threshold)
        {
            gift.transform.position = Vector3.MoveTowards(gift.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        gift.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
    }
}
