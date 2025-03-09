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

    public void Initial()
    {
        _cellsCreator.AllCellsCreated += GetListCells;
        _giftsGenerator.StartedGiftsGenerated += StartPointing;
    }

    private void GetListCells(List<Cell> cells)
    {
        _cells = cells;
    }

    private void StartPointing()
    {
        LongerChainSearcher longerChainSearcher = new LongerChainSearcher(_cells, _neighboursSearcher);
        _chainCells = longerChainSearcher.SearchLongestChain();
        Debug.Log(_chainCells.Count);

        _pointer.transform.position = _chainCells[0].transform.position;

        StartCoroutine(MoveThroughCells(_chainCells));
    }

    private IEnumerator MoveThroughCells(List<Cell> cells)
    {
        while (enabled)
        foreach (Cell cell in cells)
        {
            float targetPosX = cell.transform.position.x;
            float targetPosY = cell.transform.position.y;

            Vector3 targetPosition = new Vector2(targetPosX, targetPosY);

            while (Vector2.Distance(_pointer.transform.position, targetPosition) > _threshold)
            {
                _pointer.transform.position = Vector3.MoveTowards(_pointer.transform.position, targetPosition, _speed * Time.deltaTime);
                yield return null;
            }

            _pointer.transform.position = new Vector2(targetPosX, targetPosY);
        }
    }
}
