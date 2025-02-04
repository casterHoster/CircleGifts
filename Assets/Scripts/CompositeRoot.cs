using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private GiftsGenerator _GiftsGenerator;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private Extractor _extractor;

    private void Start()
    {
        _extractor.Initial();
        _neighboursSearcher.Initial();
        _GiftsGenerator.Initial();
    }
}
