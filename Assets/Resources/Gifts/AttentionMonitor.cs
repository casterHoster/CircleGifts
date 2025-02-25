using System;
using UnityEngine;

public class AttentionMonitor : MonoBehaviour
{
    public event Action<Cell> IsHovered;
    public event Action<Cell> IsUnhovered;
    public event Action<Cell> IsPressed;
    public event Action IsRealized;


    private void OnMouseEnter()
    {
        IsHovered?.Invoke(gameObject.GetComponent<Cell>());
    }

    private void OnMouseExit()
    {
        IsUnhovered?.Invoke(gameObject.GetComponent<Cell>());
    }

    private void OnMouseDrag()
    {
        IsPressed?.Invoke(gameObject.GetComponent<Cell>());
    }

    private void OnMouseUp()
    {
        IsRealized?.Invoke();
    }
}
