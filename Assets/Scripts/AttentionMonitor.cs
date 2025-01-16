using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionMonitor : MonoBehaviour
{
    private bool _isHovered;
    private bool _isPressed;
    private bool _isRealesed;

    private void OnMouseEnter()
    {
        _isHovered = true;
    }

    private void OnMouseExit()
    {
        _isHovered = false;
    }

    private void OnMouseDrag()
    {
        _isPressed = true;
    }

    private void OnMouseUp()
    {
        _isRealesed = true;
    }
}
