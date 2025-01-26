using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellsFabric : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private List<GiftCharacteristics> _giftCharacteristics;

    private GiftCharacteristics GetRandomCharacteristic()
    {
        var numberCharacteristic = UnityEngine.Random.Range(0, _giftCharacteristics.Count);
        return _giftCharacteristics[numberCharacteristic];
    }

    private void Characterize(Cell cell, GiftCharacteristics characteristics)
    {
        cell.SetValue(characteristics.Value);
        cell.SetSprite(characteristics.Sprite);
    }

    public Cell InstantCell(RectTransform boardTransform)
    {
        var cell = Instantiate(_cellPrefab, boardTransform); 
        Characterize(cell, GetRandomCharacteristic());
        return cell;
    }
}
