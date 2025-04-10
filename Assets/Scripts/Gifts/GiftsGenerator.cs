using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using BoardsRegulation;
using Cells;
using Gameplay;
using Monetization;
using Wrap;

namespace Gifts
{
    public class GiftsGenerator : MonoBehaviour
    {
        [SerializeField] private Background _background;
        [SerializeField] private GiftFabric _giftFabric;
        [SerializeField] private CellsCreator _cellsCreator;
        [SerializeField] private Extractor _extractor;
        [SerializeField] private WrapperViewer _wrapperViewer;
        [SerializeField] private FieldUpdater _fieldUpdater;
        [SerializeField] private Reward _reward;

        private ObjectPool<Gift> _giftPool;
        private float _collerationScalingCell = 4;
        private float _collerationScalingWrapper = 10;
        private List<Cell> _cells;

        public event Action StartedGiftsGenerated;

        public void Initial()
        {
            _giftPool = new ObjectPool<Gift>(
                createFunc: () => _giftFabric.InstantiateRandomGift(_background.RectTransform),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                defaultCapacity: 25);

            _extractor.Extracted += ReleaseFromCell;
            _cellsCreator.AllCellsCreated += AssignCellsList;
            _giftFabric.Maximised += ReleaseFromCell;
            _wrapperViewer.ValueChanged += RealeseFromUI;
            _fieldUpdater.CellUpdateNeeding += GenerateGift;
            _reward.Rewarded += RegenerateGiftsForCells;
        }

        private void OnDisable()
        {
            _cellsCreator.AllCellsCreated -= AssignCellsList;
            _extractor.Extracted -= ReleaseFromCell;
            _giftFabric.Maximised -= ReleaseFromCell;
            _wrapperViewer.ValueChanged -= RealeseFromUI;
            _fieldUpdater.CellUpdateNeeding -= GenerateGift;
            _reward.Rewarded -= RegenerateGiftsForCells;
        }

        public Gift GenerateForCanvas(int value, Transform transform)
        {
            var gift = _giftPool.Get();
            _giftFabric.TargetingCharacterize(gift, value);
            gift.transform.localScale = transform.localScale / _collerationScalingWrapper;
            gift.gameObject.SetActive(true);
            gift.gameObject.transform.position = new Vector3(
                transform.position.x, transform.position.y, gift.gameObject.transform.position.z);
            return gift;
        }

        private void AssignCellsList(List<Cell> cells)
        {
            _cells = cells;
            GenerateStartedGiftsForCells();
        }

        private void GenerateStartedGiftsForCells()
        {
            foreach (Cell cell in _cells)
            {
                GenerateGift(cell);
            }

            StartedGiftsGenerated?.Invoke();
        }

        private void RegenerateGiftsForCells()
        {
            foreach (Cell cell in _cells)
            {
                _giftPool.Release(cell.Gift);
                GenerateGift(cell);
            }
        }

        private void GenerateGift(Cell cell)
        {
            var gift = _giftPool.Get();
            cell.Fill(gift);
            _giftFabric.RandomCharacterize(gift);
            gift.transform.localScale = cell.transform.localScale / _collerationScalingCell;
            gift.gameObject.SetActive(true);
            gift.gameObject.transform.position =
                new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
        }

        private void ReleaseFromCell(Cell cell)
        {
            _giftPool.Release(cell.Gift);
            cell.ClearGift();
        }

        private void RealeseFromUI(Gift gift)
        {
            _giftPool.Release(gift);
        }
    }
}
