using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NeighboursSearcher : MonoBehaviour
{
    [SerializeField] private GiftsPool _GiftsGenerator;

    private AttentionMonitor _attentionMonitor;
    private List<Gift> _neighboursGifts;
    private float _detectionDistance = 1;

    public List<Gift> NeighboursGifts { get { return new List<Gift>(_neighboursGifts); } }

    public void Initial()
    {
        _neighboursGifts = new List<Gift>();
        _GiftsGenerator.Instantiated += SignUpMonitor;
    }


    private void OnDisable()
    {
        _attentionMonitor.IsHovered -= FindNeighbourGifts;
        _GiftsGenerator.Instantiated -= SignUpMonitor;
    }

    private void SignUpMonitor(Gift Gift)
    {
        _attentionMonitor = Gift.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsHovered += FindNeighbourGifts;
        _attentionMonitor.IsUnhovered += ClearNeighbourList;
    }

    private void FindNeighbourGifts(Gift gift)
    {
            var collider2d = gift.GetComponent<BoxCollider2D>();
            collider2d.enabled = false;

            List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();

            RaycastHit2D hitUp = Physics2D.Raycast(gift.transform.position, Vector2.up, _detectionDistance);
            raycastHits.Add(hitUp);
            RaycastHit2D hitDown = Physics2D.Raycast(gift.transform.position, Vector2.down, _detectionDistance);
            raycastHits.Add(hitDown);
            RaycastHit2D hitLeft = Physics2D.Raycast(gift.transform.position, Vector2.left, _detectionDistance);
            raycastHits.Add(hitLeft);
            RaycastHit2D hitRight = Physics2D.Raycast(gift.transform.position, Vector2.right, _detectionDistance);
            raycastHits.Add(hitRight);
            RaycastHit2D hitUpRight = Physics2D.Raycast(gift.transform.position, Vector2.up + Vector2.right, _detectionDistance);
            raycastHits.Add(hitUpRight);
            RaycastHit2D hitUpLeft = Physics2D.Raycast(gift.transform.position, Vector2.up + Vector2.left, _detectionDistance);
            raycastHits.Add(hitUpLeft);
            RaycastHit2D hitDownLeft = Physics2D.Raycast(gift.transform.position, Vector2.down + Vector2.left, _detectionDistance);
            raycastHits.Add(hitDownLeft);
            RaycastHit2D hitDownRight = Physics2D.Raycast(gift.transform.position, Vector2.down + Vector2.right, _detectionDistance);
            raycastHits.Add(hitDownRight);   

            foreach (var hit in raycastHits)
            {
                if (hit.collider != null && hit.collider.TryGetComponent(out Gift thisGift) && thisGift.Value == gift.Value)
                {
                    _neighboursGifts.Add(thisGift);
                }
            }

            collider2d.enabled = true;
    }

    private void ClearNeighbourList(Gift Gift)
    {
        _neighboursGifts.Clear();
    }
}
