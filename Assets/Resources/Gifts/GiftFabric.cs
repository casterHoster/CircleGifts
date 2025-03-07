using System;
using System.Collections.Generic;
using UnityEngine;

public class GiftsFabric : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Gift _giftPrefab;
    [SerializeField] private Extractor _extractor;
    [SerializeField] private CellsAnalyser _cellAnalyser;
    [SerializeField] private List<GiftCharacteristics> _giftCharacteristics;
    [SerializeField] private int _strartedGeneratingCharacteristicsCount = 3; 

    private List<GiftCharacteristics> _generatingCharacteristics;

    public Action<Cell> Maximised;

    public void Initial()
    {
        InitStartedGiftChahcteristics();
        _extractor.EndCellDifined += ImproveGift;
        _cellAnalyser.HightestValueFound += AddCharacteristic;
    }

    private void OnDisable()
    {
        _extractor.EndCellDifined -= ImproveGift;
        _cellAnalyser.HightestValueFound -= AddCharacteristic;
    }

    public void RandomCharacterize(Gift gift)
    {
        GiftCharacteristics characteristic = GetRandomCharacteristic();

        gift.SetValue(characteristic.Value);
        gift.SetSprite(characteristic.Sprite);
    }

    public void TargetingCharacterize(Gift gift, int value)
    {
        GiftCharacteristics characteristic = SearchCharacteristic(value);

        if (characteristic != null)
        {
            gift.SetValue(characteristic.Value);
            gift.SetSprite(characteristic.Sprite);
        }
        else
        {
            gift.SetSprite(_defaultSprite);
        }
    }

    public Gift InstantiateRandomGift(RectTransform boardTransform)
    {
        var gift = Instantiate(_giftPrefab, boardTransform);
        RandomCharacterize(gift);
        return gift;
    }

    private void InitStartedGiftChahcteristics()
    {
        _generatingCharacteristics = new List<GiftCharacteristics>();

        if (_strartedGeneratingCharacteristicsCount > 0 && _strartedGeneratingCharacteristicsCount < _giftCharacteristics.Count)
        {
            for (int i = 0; i < _strartedGeneratingCharacteristicsCount; i++) 
            {
                _generatingCharacteristics.Add(_giftCharacteristics[i]);
            }
        }
    }

    private void AddCharacteristic(int value)
    {
        foreach (var characteristic in _generatingCharacteristics)
        {
            if (characteristic.Value == value)
            {
                return;
            }
        }

        _generatingCharacteristics.Add(SearchCharacteristic(value));
    }

    private GiftCharacteristics GetRandomCharacteristic()
    {
        var numberCharacteristic = UnityEngine.Random.Range(0, _generatingCharacteristics.Count);
        return _generatingCharacteristics[numberCharacteristic];
    }

    private void ImproveGift(Cell cell, int countCells)
    {
        int value = cell.Gift.Value;

        for (int i = 0; i < countCells - 1; i++)
        {
            value = value * 2;
        }

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
