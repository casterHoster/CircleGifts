using System;
using System.Collections.Generic;
using UnityEngine;
using Cells;
using Gifts;

namespace Gameplay
{
    public class Extractor : MonoBehaviour
    {
        [SerializeField] private CellsCreator _cellsCreator;
        [SerializeField] private NeighboursSearcher _searcher;

        private AttentionMonitor _attentionMonitor;
        private List<Cell> _chainedCells;
        private List<Cell> _neighbourCells;
        private bool _isPressed;

        public event Action<Cell> Extracted;
        public event Action<Cell, int> EndCellDifined;
        public event Action<Cell> EndCellPutOuted;
        public event Action<List<Cell>> ListIsDefined;
        public event Action<Cell> PutOuted;
        public event Action<Cell> CellAdded;
        public event Action CellRemoved;

        public void Initial()
        {
            _chainedCells = new List<Cell>();
            _cellsCreator.AllCellsCreated += SignUpMonitors;
        }

        private void OnDisable()
        {
            _cellsCreator.AllCellsCreated -= SignUpMonitors;
            _attentionMonitor.IsPressed -= AddPressed;
            _attentionMonitor.IsRealized -= ExtractCells;
            _attentionMonitor.IsUnhovered -= SaveNeighboursByUnvovering;
            _attentionMonitor.IsHovered -= AddHovered;
        }

        private void SignUpMonitors(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                _attentionMonitor = cell.gameObject.GetComponent<AttentionMonitor>();
                _attentionMonitor.IsPressed += AddPressed;
                _attentionMonitor.IsRealized += ExtractCells;
                _attentionMonitor.IsUnhovered += SaveNeighboursByUnvovering;
                _attentionMonitor.IsHovered += AddHovered;
            }
        }

        private void AddPressed(Cell cell)
        {
            if (_isPressed == false)
            {
                _chainedCells.Add(cell);
                SaveNeighboursByClicking();
                CellAdded?.Invoke(cell);
                _isPressed = true;
            }
        }

        private void AddHovered(Cell cell)
        {
            if (_isPressed && cell.Gift != null)
            {
                if (CheckMatchWithNeighbours(cell))
                {
                    if (_chainedCells.Contains(cell))
                    {
                        PutOuted?.Invoke(_chainedCells[_chainedCells.Count - 1]);
                        _chainedCells.RemoveAt(_chainedCells.Count - 1);
                        CellRemoved?.Invoke();
                    }
                    else
                    {
                        _chainedCells.Add(cell);
                        CellAdded?.Invoke(cell);
                    }
                }
            }
        }

        private void ExtractCells()
        {
            if (_chainedCells.Count > 1)
            {
                ListIsDefined?.Invoke(_chainedCells);
                Cell lastCell = _chainedCells[_chainedCells.Count - 1];
                EndCellDifined?.Invoke(lastCell, _chainedCells.Count);
                EndCellPutOuted?.Invoke(lastCell);
                _chainedCells.Remove(lastCell);

                foreach (var cell in _chainedCells)
                {
                    Extracted?.Invoke(cell);
                }
            }
            else if (_chainedCells.Count > 0)
            {
                PutOuted?.Invoke(_chainedCells[_chainedCells.Count - 1]);
            }

            _chainedCells.Clear();
            _isPressed = false;
        }

        private void SaveNeighboursByClicking()
        {
            _neighbourCells = _searcher.NeighboursCells;
        }

        private void SaveNeighboursByUnvovering(Cell cell)
        {
            if (_isPressed)
            {
                if (cell.Gift != null && _chainedCells[_chainedCells.Count - 1].Gift != null &&
                    cell.Gift.Value == 
                    _chainedCells[_chainedCells.Count - 1].Gift.Value && _neighbourCells.Contains(cell))
                {
                    _neighbourCells = _searcher.NeighboursCells;
                }
            }
        }

        private bool CheckMatchWithNeighbours(Cell cell)
        {
            foreach (Cell neighbourCell in _neighbourCells)
            {
                if (neighbourCell != null)
                {
                    if (cell.Gift != null && _chainedCells[_chainedCells.Count - 1].Gift &&
                        _chainedCells[_chainedCells.Count - 1].Gift.Value == 
                        cell.Gift.Value && cell.gameObject == neighbourCell.gameObject)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
