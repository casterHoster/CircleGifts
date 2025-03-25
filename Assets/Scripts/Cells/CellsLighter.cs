using UnityEngine;
using UnityEngine.UI;
using Gameplay;
using Training;

namespace Cells
{
    [RequireComponent(typeof(Image))]
    public class CellsLighter : MonoBehaviour
    {
        [SerializeField] private Extractor _extractor;
        [SerializeField] private PathPointer _pathPointer;

        public void Initial()
        {
            _pathPointer.CellReached += EnableSprite;
            _pathPointer.CircleOvered += DisableSprite;
            _extractor.CellAdded += EnableSprite;
            _extractor.EndCellDifined += DisableSprite;
            _extractor.Extracted += DisableSprite;
            _extractor.PutOuted += DisableSprite;
        }

        private void OnDisable()
        {
            _pathPointer.CellReached -= EnableSprite;
            _pathPointer.CircleOvered -= DisableSprite;
            _extractor.CellAdded -= EnableSprite;
            _extractor.EndCellDifined -= DisableSprite;
            _extractor.Extracted -= DisableSprite;
            _extractor.PutOuted -= DisableSprite;
        }

        private void EnableSprite(Cell cell)
        {
            cell.ImageLight.enabled = true;
        }

        private void DisableSprite(Cell cell)
        {
            cell.ImageLight.enabled = false;
        }

        private void DisableSprite(Cell cell, int count)
        {
            cell.ImageLight.enabled = false;
        }
    }
}
