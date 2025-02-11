using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiftsFabric : MonoBehaviour
{
    [SerializeField] private Gift _giftPrefab;
    [SerializeField] Extractor _extractor;
    [SerializeField] private List<GiftCharacteristics> _giftCharacteristics;

    public Action<Cell> Maximised;

    public void Initial()
    {
        _extractor.Lasted += Improve;
    }

    private GiftCharacteristics GetRandomCharacteristic()
    {
        var numberCharacteristic = UnityEngine.Random.Range(0, _giftCharacteristics.Count);
        return _giftCharacteristics[numberCharacteristic];
    }

    private void Improve(Cell cell)
    {
        int value = cell.Gift.Value;

        value *= 2;

        GiftCharacteristics characteristic = SearchCharacteristic(value);

        if (characteristic != null) 
        {
            cell.Gift.SetValue(characteristic.Value);
            cell.Gift.SetSprite(characteristic.Sprite);
        }
        else
        {
            Maximised?.Invoke(cell);
        }
    }



    public void Characterize(Gift gift)
    {
        GiftCharacteristics characteristic = GetRandomCharacteristic();

        gift.SetValue(characteristic.Value);
        gift.SetSprite(characteristic.Sprite);
    }


    public Gift InstantGift(RectTransform boardTransform)
    {
        var gift = Instantiate(_giftPrefab, boardTransform);
        Characterize(gift);
        return gift;
    }

    private void Characterize(Gift gift, GiftCharacteristics characteristic)
    {
        gift.SetValue(characteristic.Value);
        gift.SetSprite(characteristic.Sprite);
    }

    private GiftCharacteristics SearchCharacteristic(int value)
    {
        foreach (var characteristic in _giftCharacteristics)
        {
            if (characteristic.Value == value)
                return characteristic;
        }

        return null;
    }
}
