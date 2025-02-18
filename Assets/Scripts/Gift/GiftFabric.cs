using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiftsFabric : MonoBehaviour
{
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
        _extractor.EndCellDifined += Improve;
        _cellAnalyser.HightestValueFound += AddCharacteristicForGenerate;
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

    private void AddCharacteristicForGenerate(int value)
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
