using System;
using UnityEngine;

public class AttentionMonitor : MonoBehaviour
{
    public event Action<Gift> IsHovered;
    public event Action<Gift> IsUnhovered;
    public event Action<Gift> IsPressed;
    public event Action IsRealized;


    private void OnMouseEnter()
    {
        IsHovered?.Invoke(gameObject.GetComponent<Gift>());
    }

    private void OnMouseExit()
    {
        IsUnhovered?.Invoke(gameObject.GetComponent<Gift>());
    }

    private void OnMouseDrag()
    {
        IsPressed?.Invoke(gameObject.GetComponent<Gift>());
    }

    private void OnMouseUp()
    {
        IsRealized?.Invoke();
    }
}
