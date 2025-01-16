using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellsFabric : MonoBehaviour
{
    [SerializeField] private List<Cell> _cellsPrefabs;

    public Cell GetRandomCell()
    {
        var cellNum = UnityEngine.Random.Range(0, _cellsPrefabs.Count);
        return _cellsPrefabs[cellNum];
    }

    public Cell InstantCell(RectTransform boardTransform)
    {
        var cell = Instantiate(GetRandomCell(), boardTransform);
        return cell;
    }
}
