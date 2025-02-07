using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiftsFabric : MonoBehaviour
{
    [SerializeField] private Gift _giftPrefab;
    [SerializeField] private List<GiftCharacteristics> _giftCharacteristics;

    private GiftCharacteristics GetRandomCharacteristic()
    {
        var numberCharacteristic = UnityEngine.Random.Range(0, _giftCharacteristics.Count);
        return _giftCharacteristics[numberCharacteristic];
    }

    private void Characterize(Gift gift, GiftCharacteristics characteristics)
    {
        gift.SetValue(characteristics.Value);
        gift.SetSprite(characteristics.Sprite);
    }

    public Gift InstantGift(RectTransform boardTransform)
    {
        var gift = Instantiate(_giftPrefab, boardTransform); 
        gift.SetRectTransform(boardTransform);
        Characterize(gift, GetRandomCharacteristic());
        return gift;
    }
}
