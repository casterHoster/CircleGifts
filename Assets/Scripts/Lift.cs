using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private GiftsPool _giftsPool;

    private float _detectionDistance = 1;
    private int _layerMask;

    private void OnEnable()
    {

    }

    private void RelocateGift(Cell cell)
    {
        RaycastHit2D hitUp = Physics2D.Raycast(cell.transform.position, Vector2.up, _detectionDistance);

        if (hitUp.collider != null && hitUp.collider.TryGetComponent(out Cell upCell))
        {
            
        }
    }
}
