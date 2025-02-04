using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private GiftsGenerator _giftsGenerator;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private Extractor _extractor;
    [SerializeField] private FieldCreator _fieldCreator;

    private void Start()
    {
        _extractor.Initial();
        _neighboursSearcher.Initial();
        _giftsGenerator.Initial();
        _fieldCreator.Initial();
    }
}
