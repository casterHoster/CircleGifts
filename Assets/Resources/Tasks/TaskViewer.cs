using System;
using TMPro;
using UnityEngine;

public class TaskViewer : MonoBehaviour
{
    [SerializeField] TaskRegulator _taskRegulator;
    [SerializeField] GiftsGenerator _giftsGenerator;
    [SerializeField] GiftsFabric _giftsFabric;
    [SerializeField] TextMeshProUGUI _giftsCountText;
    [SerializeField] TextMeshProUGUI _movesCountText;

    private Gift _currentGift;

    public Action<Gift> ValueChanged;

    private void OnEnable()
    {
        _taskRegulator.TaskSet += DrawTask;
        _taskRegulator.Compared += ChangeTaskValues;
    }

    private void DrawTask(TargetTask task)
    {
        if (_currentGift == null)
        { 
            _currentGift = _giftsGenerator.GenerateForCanvas(task.GiftValue, transform);
        }
        else
        {
            _giftsFabric.TargetingCharacterize(_currentGift, task.GiftValue);
        }

        _giftsCountText.text = task.GiftsCount.ToString();
        _movesCountText.text = task.Moves.ToString();
    }

    private void ChangeTaskValues(int leftGiftsCount, int leftMovesCount)
    {
        _giftsCountText.text = leftGiftsCount.ToString();
        _movesCountText.text = leftMovesCount.ToString();
    }
}
