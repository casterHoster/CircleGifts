using System;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    [SerializeField] private GiftsPool _giftsGenerator;
    [SerializeField] private NeighboursSearcher _searcher;

    private AttentionMonitor _attentionMonitor;
    private List<Gift> _markedGifts;
    private List<Gift> _neighboursGifts;
    private bool _isPressed;

    public Action<Gift> Extracted;

    public void Initial()
    {
        _markedGifts = new List<Gift>();
        _giftsGenerator.Instantiated += SignUpMonitor;
    }

    private void OnDisable()
    {
        _giftsGenerator.Instantiated -= SignUpMonitor;
        _attentionMonitor.IsPressed -= AddPressed;
        _attentionMonitor.IsRealized -= ExtractGifts;
    }

    private void SignUpMonitor(Gift Gift)
    {
        _attentionMonitor = Gift.gameObject.GetComponent<AttentionMonitor>();
        _attentionMonitor.IsPressed += AddPressed;
        _attentionMonitor.IsRealized += ExtractGifts;
        _attentionMonitor.IsUnhovered += SaveNeighbours;
        _attentionMonitor.IsHovered += TryAddHovered;
    }

    private void AddPressed(Gift Gift)
    {
        if (_isPressed == false)
        {
            _markedGifts.Add(Gift);
            _isPressed = true;
        }
    }

    private void TryAddHovered(Gift gift)
    {
        if (_isPressed)
        {
            if (CheckMatch(gift) && !_markedGifts.Contains(gift))
                _markedGifts.Add(gift);

            TryRemoveUnselected(gift);
        }
    }

    private void ExtractGifts()
    {
        if (_markedGifts.Count > 1)
        {
            foreach (var gift in _markedGifts)
            {
                if (gift != null)
                {
                    Extracted?.Invoke(gift);
                }
            }
        }

        _markedGifts.Clear();
        _isPressed = false;
    }

    private void SaveNeighbours(Gift Gift)
    {
        if (!_isPressed)
            _neighboursGifts = _searcher.NeighboursGifts;
        else
        {
            if (Gift.Value == _markedGifts[_markedGifts.Count - 1].Value)
                _neighboursGifts = _searcher.NeighboursGifts;
        }
            
    }

    private bool CheckMatch(Gift gift)
    {
        foreach (Gift neighbourGift in _neighboursGifts)
        {
            if (neighbourGift != null)
            {
                if (_markedGifts[_markedGifts.Count - 1].Value == gift.Value && gift.gameObject == neighbourGift.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void TryRemoveUnselected(Gift gift)
    {
        if (_markedGifts.Count - 2 >= 0 && gift == _markedGifts[_markedGifts.Count - 2])
        {
            _markedGifts.RemoveAt(_markedGifts.Count - 2);
        }
    }
}
