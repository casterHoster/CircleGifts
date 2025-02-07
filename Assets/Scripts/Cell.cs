using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    public Action<Cell> Emptyed;

    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void PassEmpty()
    {
        Emptyed?.Invoke(this);
    }
}
