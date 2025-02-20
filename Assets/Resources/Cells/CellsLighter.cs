using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellsLighter : MonoBehaviour
{
    [SerializeField] private Extractor _extractor;

    public void Initial()
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
    private void DisableSprite(Cell cell, int count)
    {
        cell.GetComponent<SpriteRenderer>().enabled = false;
    }
}
