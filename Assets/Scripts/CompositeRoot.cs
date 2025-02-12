using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private GiftsPool _giftsPool;
    [SerializeField] private GiftsFabric _giftsFabric;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private Extractor _extractor;
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private FieldOperator _operator;
    [SerializeField] private CellsAnalyser _cellsAnalyser;

    private void Start()
    {
        _extractor.Initial();
        _giftsFabric.Initial();
        _neighboursSearcher.Initial();
        _giftsPool.Initial();
        _operator.Initial();
        _cellsAnalyser.Initial();
        _cellsCreator.Initial();
    }
}
