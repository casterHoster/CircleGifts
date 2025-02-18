using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LighterCells : MonoBehaviour
{
    [SerializeField] private Extractor _extractor;

    private void OnEnable()
    {
        _extractor.CellAdded += EnableSprite;
        _extractor.EndCellDifined += DisableSprite;
        _extractor.Extracted += DisableSprite;
        _extractor.PutOuted += DisableSprite;
    }

    private void OnDisable()
    {
        _extractor.CellAdded -= EnableSprite;
        _extractor.EndCellDifined -= DisableSprite;
        _extractor.Extracted -= DisableSprite;
        _extractor.PutOuted -= DisableSprite;
    }

    private void EnableSprite(Cell cell)
    {
        cell.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void DisableSprite(Cell cell)
    {
        cell.GetComponent<SpriteRenderer>().enabled = false;
    }
}
