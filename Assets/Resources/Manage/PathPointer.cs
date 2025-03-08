using System.Collections.Generic;
using UnityEngine;

public class PathPointer : MonoBehaviour
{
    [SerializeField] GameObject _pointer;
    [SerializeField] private CellStorage _cellStorage;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private float _speed;

    private List<Cell> _cells = new List<Cell>();
    private List<Cell> _chainCells;

    private void OnEnable()
    {

    }

    private void StartPointing()
    {
        LongerChainSearcher longerChainSearcher = new LongerChainSearcher(_cells, _neighboursSearcher);
        _chainCells = longerChainSearcher.SearchLongestChain();
        Debug.Log(_chainCells.Count);

        _pointer.transform.position = _chainCells[0].transform.position;

        foreach (Cell cell in _chainCells) 
        {
            _pointer.transform.position = Vector3.MoveTowards(_pointer.transform.position, cell.transform.position, _speed * Time.deltaTime);
        }
    }
}
