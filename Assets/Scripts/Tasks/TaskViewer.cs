using TMPro;
using UnityEngine;
using Gifts;

namespace Tasks
{
    public class TaskViewer : MonoBehaviour
    {
        [SerializeField] private TaskRegulator _taskRegulator;
        [SerializeField] private GiftsGenerator _giftsGenerator;
        [SerializeField] private GiftFabric _giftsFabric;
        [SerializeField] private TextMeshProUGUI _giftsCountText;
        [SerializeField] private TextMeshProUGUI _movesCountText;

        private Gift _currentGift;

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
}
