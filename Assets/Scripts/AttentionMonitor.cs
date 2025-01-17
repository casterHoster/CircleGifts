using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionMonitor : MonoBehaviour
{
    public event Action IsHovered;
    public event Action IsUnhovered;
    public event Action IsPressed;
    public event Action IsRealized;


    private void OnMouseEnter()
    {
        IsHovered?.Invoke();
    }

    private void OnMouseExit()
    {
        IsUnhovered?.Invoke();
    }

    private void OnMouseDrag()
    {
        IsPressed?.Invoke();
    }

    private void OnMouseUp()
    {
        IsRealized?.Invoke();
    }
}
