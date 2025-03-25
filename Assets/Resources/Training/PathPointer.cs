using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointer : MonoBehaviour
{
    [SerializeField] GameObject _pointer;
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private GiftsGenerator _giftsGenerator;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private float _speed;

    private List<Cell> _cells;
    private List<Cell> _chainCells;
    private float _threshold = 0.1f;
    private bool _isEnabled;

    public event Action<Cell> CellReached;
    public event Action<Cell> CircleOvered;

    public void Initial()
    {
        _cellsCreator.AllCellsCreated += GetListCells;
        _giftsGenerator.StartedGiftsGenerated += StartPointing;
    }

    private void OnDisable()
    {
        _cellsCreator.AllCellsCreated -= GetListCells;
        _giftsGenerator.StartedGiftsGenerated -= StartPointing;
    }

    private void GetListCells(List<Cell> cells)
    {
        _cells = cells;

        foreach (Cell cell in _cells)
        {
            cell.GetComponent<AttentionMonitor>().IsPressed += SwitchPointEnable;
        }
    }

    private void StartPointing()
    {
        LongerChainSearcher longerChainSearcher = new LongerChainSearcher(_cells, _neighboursSearcher);
        _chainCells = longerChainSearcher.SearchLongestChain();
        _pointer.transform.position = _chainCells[0].transform.position;
        StartCoroutine(MoveThroughCells());
    }

    private IEnumerator MoveThroughCells()
    {
        _isEnabled = true;

        while (_isEnabled)
        {
            _pointer.SetActive(true);
            _pointer.transform.position = _chainCells[0].transform.position;

            foreach (Cell cell in _chainCells)
            {
                if (_isEnabled == false)
                {
                    foreach (Cell theCell in _chainCells)
                    {
                        CircleOvered?.Invoke(theCell);
                    }

                    _pointer.SetActive(false);
                    yield break;
                }

                float targetPosX = cell.transform.position.x;
                float targetPosY = cell.transform.position.y;

                Vector3 targetPosition = new Vector2(targetPosX, targetPosY);

                while (Vector2.Distance(_pointer.transform.position, targetPosition) > _threshold)
                {
                    _pointer.transform.position = Vector3.MoveTowards(_pointer.transform.position, targetPosition, _speed * Time.deltaTime);
                    yield return null;
                }

                CellReached?.Invoke(cell);
                _pointer.transform.position = new Vector2(targetPosX, targetPosY);
            }

            foreach (Cell cell in _chainCells)
            {
                CircleOvered?.Invoke(cell);
            }

            _pointer.SetActive(false);
        }
    }

    private void SwitchPointEnable(Cell anycell)
    {
        _isEnabled = false;
        StopCoroutine(MoveThroughCells());
    }
}
