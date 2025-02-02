using System;
using UnityEngine;

public class AttentionMonitor : MonoBehaviour
{
    public event Action<GameObject> IsHovered;
    public event Action<GameObject> IsUnhovered;
    public event Action<GameObject> IsPressed;
    public event Action IsRealized;


    private void OnMouseEnter()
    {
        IsHovered?.Invoke(gameObject);
    }

    private void OnMouseExit()
    {
        IsUnhovered?.Invoke(gameObject);
    }

    private void OnMouseDrag()
    {
        IsPressed?.Invoke(gameObject);
    }

    private void OnMouseUp()
    {
        IsRealized?.Invoke();
    }
}
