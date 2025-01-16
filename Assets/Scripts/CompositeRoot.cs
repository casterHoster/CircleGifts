using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private CellsGenerator _cellsGenerator;

    private void Start()
    {
        _cellsGenerator.Initial();
    }
}
