using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOperator : MonoBehaviour
{
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private GiftsPool _giftsPool;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _generateDelay;
    [SerializeField] private float _detectionDistance = 1;

    private float threshold = 0.15f;
    private Queue<Cell> _cells;
    private WaitForSeconds _cooldownChecking = new WaitForSeconds(0.1f);
    private Action<Cell> _cellClearedHandler;

    public Action Reformed;

    public void Initial()
    {
        _cells = new Queue<Cell>();
        StartCoroutine(CheckQueueCells());
        _cellsCreator.CellCreated += SignUpClearing;
    }

    private void OnDisable()
    {
        _cellsCreator.CellCreated -= SignUpClearing;

        if (_cellClearedHandler != null)
        {
            foreach (var cell in _cells)
            {
                cell.Cleared -= _cellClearedHandler;
            }
        }
    }

    private void SignUpClearing(Cell cell)
    {
        _cellClearedHandler = AddCellQueue;
        cell.Cleared += AddCellQueue;
    }

    private void AddCellQueue(Cell cell)
    {
        if (!_cells.Contains(cell))
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
        collider2d.enabled = true;

        if (hitUp.collider != null)
        {
            if (hitUp.collider.TryGetComponent(out Cell upCell) && !_cells.Contains(upCell) && upCell.Gift != null && cell.Gift == null)
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
            if (cell.Gift == null)
                StartCoroutine(GenerateGiftWithDelay(cell));
            else
                _cells.Enqueue(cell);
        }

        if (_cells.Count == 0)
        {
            Reformed?.Invoke();
        }
    }

    private IEnumerator GenerateGiftWithDelay(Cell cell)
    {
        yield return new WaitForSeconds(_generateDelay);

        _giftsPool.GenerateForCell(cell);
        cell.Gift.transform.position =
            new Vector3(cell.Gift.gameObject.transform.position.x, cell.Gift.gameObject.transform.position.y + 1, cell.Gift.gameObject.transform.position.z);
        StartCoroutine(Move(cell.Gift, cell));
    }

    private IEnumerator Move(Gift gift, Cell cell)
    {
        float targetPosX = cell.transform.position.x;
        float targetPosY = cell.transform.position.y;
        float targetPosZ = gift.transform.position.z;


        Vector3 finishPosittion = cell.transform.position;
        Vector3 targetPosition = new Vector3(targetPosX, targetPosY, targetPosZ);

        while (Vector3.Distance(gift.transform.position, finishPosittion) > threshold)
        {
            gift.transform.position = Vector3.MoveTowards(gift.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        gift.transform.position = new Vector3(targetPosX, targetPosY, targetPosZ);
    }
}
