using System;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    [SerializeField] private GiftsGenerator _GiftsGenerator;
    [SerializeField] private NeighboursSearcher _searcher;

    private AttentionMonitor _attentionMonitor;
    private List<Gift> _markedGifts;
    private List<Gift> _neighboursGifts;
    private bool _isPressed;

    public void Initial()
    {
        _markedGifts = new List<Gift>();
        _GiftsGenerator.Instantiated += SignUpMonitor;
    }

    private void OnDisable()
    {
        _GiftsGenerator.Instantiated -= SignUpMonitor;
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

    private void TryAddHovered(Gift Gift)
    {
        if (_isPressed)
        {
            if (CheckMatch(Gift))
                _markedGifts.Add(Gift);
        }
    }

    private void ExtractGifts()
    {
        if (_markedGifts.Count > 1)
        {
            foreach (var Gift in _markedGifts)
            {
                if (Gift != null)
                {
                    Destroy(Gift.gameObject);
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

    private bool CheckMatch(Gift Gift)
    {
        foreach (Gift neighbourGift in _neighboursGifts)
        {
            if (neighbourGift != null)
            {
                if (_markedGifts[_markedGifts.Count - 1].Value == Gift.Value && Gift.gameObject == neighbourGift.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
