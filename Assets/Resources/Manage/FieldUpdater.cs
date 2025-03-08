using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldUpdater : MonoBehaviour
{
    [SerializeField] private CellStorage _cellStorage;
    [SerializeField] private GiftsPool _giftsPool;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _generateDelay;
    [SerializeField] private float _detectionDistance = 1;

    private float _threshold = 0.1f;
    private Queue<Cell> _clearCells;
    private WaitForSeconds _cooldownChecking = new WaitForSeconds(0.1f);

    public Action Reformed;
    public Action<Cell> NeededCellUpdate;
    private Action<Cell> _cellClearedHandler;

    public void Initial()
    {
        _clearCells = new Queue<Cell>();
        StartCoroutine(CheckQueueCells());
        _cellStorage.ListFormed += SignUpClearing;
    }

    private void OnDisable()
    {
        _cellStorage.ListFormed -= SignUpClearing;

        if (_cellClearedHandler != null)
        {
            foreach (var cell in _clearCells)
            {
                cell.Cleared -= _cellClearedHandler;
            }
        }
    }

    private void SignUpClearing(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            _cellClearedHandler = AddCellQueue;
            cell.Cleared += AddCellQueue;
        }
    }

    private void AddCellQueue(Cell cell)
    {
        if (!_clearCells.Contains(cell))
            _clearCells.Enqueue(cell);
    }

    private IEnumerator CheckQueueCells()
    {
        while (enabled)
        {
            if (_clearCells.Count > 0)
            {
                Reform();
            }

            yield return _cooldownChecking;
        }
    }

    private void Reform()
    {
        Cell cell = _clearCells.Dequeue();
        var collider2d = cell.GetComponent<BoxCollider2D>();
        collider2d.enabled = false;

        RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, _detectionDistance);
        collider2d.enabled = true;

        if (hitUp.collider != null)
        {
            if (hitUp.collider.TryGetComponent(out Cell upCell) && !_clearCells.Contains(upCell) && upCell.Gift != null && cell.Gift == null)
            {
                cell.Fill(upCell.Gift);
                upCell.Clear();
                StartCoroutine(MoveGifts(cell.Gift, cell));
            }
            else
            {
                _clearCells.Enqueue(cell);
            }
        }
        else
        {
            if (cell.Gift == null)
                StartCoroutine(UpdateCellsGiftWithDelay(cell));
            else
                _clearCells.Enqueue(cell);
        }

        if (_clearCells.Count == 0)
        {
            Reformed?.Invoke();
        }
    }

    private IEnumerator UpdateCellsGiftWithDelay(Cell cell)
    {
        yield return new WaitForSeconds(_generateDelay);

        NeededCellUpdate?.Invoke(cell);
        cell.Gift.transform.position =
            new Vector3(cell.Gift.gameObject.transform.position.x, cell.Gift.gameObject.transform.position.y + 1, cell.Gift.gameObject.transform.position.z);
        StartCoroutine(MoveGifts(cell.Gift, cell));
    }

    private IEnumerator MoveGifts(Gift gift, Cell cell)
    {
        float targetPosX = cell.transform.position.x;
        float targetPosY = cell.transform.position.y;
        float targetPosZ = gift.transform.position.z;


        Vector3 finishPosittion = cell.transform.position;
        Vector3 targetPosition = new Vector3(targetPosX, targetPosY, targetPosZ);

        while (Vector3.Distance(gift.transform.position, finishPosittion) > _threshold)
        {
            gift.transform.position = Vector3.MoveTowards(gift.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        gift.transform.position = new Vector3(targetPosX, targetPosY, targetPosZ);
    }
}
