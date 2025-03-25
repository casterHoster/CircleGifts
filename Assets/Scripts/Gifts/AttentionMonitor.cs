using System;
using UnityEngine;
using Cells;

namespace Gifts
{
    public class AttentionMonitor : MonoBehaviour
    {
        private Cell _cell;

        public event Action<Cell> IsHovered;
        public event Action<Cell> IsUnhovered;
        public event Action<Cell> IsPressed;
        public event Action IsRealized;

        private void Awake()
        {
            _cell = GetComponent<Cell>();
        }

        private void OnMouseEnter()
        {
            IsHovered?.Invoke(_cell);
        }

        private void OnMouseExit()
        {
            IsUnhovered?.Invoke(_cell);
        }

        private void OnMouseDrag()
        {
            IsPressed?.Invoke(_cell);
        }

        private void OnMouseUp()
        {
            IsRealized?.Invoke();
        }
    }
}
