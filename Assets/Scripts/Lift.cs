using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] FieldCreator _fieldCreator;

    private float _detectionDistance = 10;
    private float _speed = 2;
    private bool _isMoving = false;

    float threshold = 0.1f;

    private void OnEnable()
    {
        _fieldCreator.CellCreated += SubscribeChangeEmpty;
    }

    private void SubscribeChangeEmpty(Cell cell)
    {
        cell.Emptyed += RelocateGift;
    }

    private void RelocateGift(Cell cell)
    {
        Cell currentCell = cell;

        while (currentCell != null)
        {
            RaycastHit2D hitUp = Physics2D.Raycast(currentCell.transform.position, Vector2.up, _detectionDistance);


            if (hitUp.collider != null && hitUp.collider.TryGetComponent(out Gift gift))
            {
                Cell upCell = gift.transform.GetComponentInParent<Cell>();
                gift.GetComponent<BoxCollider2D>().enabled = false;

                if (!_isMoving)
                {
                    gift.transform.SetParent(currentCell.transform);
                    StartCoroutine(Move(gift, currentCell));
                    _isMoving = true;
                }
                
                currentCell = upCell;
            }
            else
            {
                break;
            }
        }
    }

    private IEnumerator Move(Gift gift, Cell cell)
    {
        while (Vector3.Distance(gift.transform.position, cell.transform.position) > threshold)
        {
            Vector3 targetPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
            gift.transform.position = Vector3.MoveTowards(gift.transform.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }

        gift.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
        _isMoving = false;
        gift.GetComponent<BoxCollider2D>().enabled = true;
    }
}
