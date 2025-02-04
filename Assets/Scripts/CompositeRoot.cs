using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private CellsGenerator _cellsGenerator;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private Extractor _extractor;

    private void Start()
    {
        _extractor.Initial();
        _neighboursSearcher.Initial();
        _cellsGenerator.Initial();
    }
}
