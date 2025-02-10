using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private GiftsPool _giftsGenerator;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private Extractor _extractor;
    [SerializeField] private FieldCreator _fieldCreator;
    [SerializeField] private Lift _lift;

    private void Start()
    {
        _extractor.Initial();
        _neighboursSearcher.Initial();
        _giftsGenerator.Initial();
        _lift.Initial();
        _fieldCreator.Initial();
    }
}
