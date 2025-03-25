using System;
using System.Collections.Generic;
using UnityEngine;
using Cells;
using Gameplay;

namespace Wrap
{
    public class Wrapper : MonoBehaviour
    {
        [SerializeField] private Extractor _extractor;

        private List<Cell> _cells;

        public event Action<int> Counted;

        public void Initial()
        {
            _cells = new List<Cell>();
            _extractor.CellAdded += AddCell;
            _extractor.EndCellDifined += RemoveCell;
            _extractor.Extracted += RemoveCell;
            _extractor.PutOuted += RemoveCell;
        }

        private void OnDisable()
        {
            _extractor.CellAdded -= AddCell;
            _extractor.EndCellDifined -= RemoveCell;
            _extractor.Extracted -= RemoveCell;
            _extractor.PutOuted -= RemoveCell;
        }

        private void AddCell(Cell cell)
        {
            _cells.Add(cell);
            CountNewWrapValue(cell);
        }

        private void RemoveCell(Cell cell)
        {
            _cells.Remove(cell);
            CountNewWrapValue(cell);
        }

        private void RemoveCell(Cell cell, int count)
        {
            _cells.Remove(cell);
            CountNewWrapValue(cell);
        }

        private void CountNewWrapValue(Cell cell)
        {
            int value = 0;

            if (_cells.Count > 0 && _cells[_cells.Count - 1].Gift != null)
            {
                value = _cells[_cells.Count - 1].Gift.Value;

                for (int i = 0; i < _cells.Count - 1; i++)
                {
                    value = value * 2;
                }
            }

            Counted?.Invoke(value);
        }
    }
}
