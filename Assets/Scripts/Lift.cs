using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Extractor _extractor;

    private float _detectionDistance = 1;

    private void OnEnable()
    {
        _extractor.Extracted += RelocateGift;
    }

    private void RelocateGift(Cell cell)
    {
        RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, _detectionDistance);

        if (hitUp.collider != null && hitUp.collider.TryGetComponent(out Cell upCell))
        {
            
        }
    }
}
