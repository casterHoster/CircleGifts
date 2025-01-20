using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(AttentionMonitor))]
[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public int Value { get; protected set; }
    public RectTransform RectTransform { get; private set; }

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        RectTransform = GetComponent<RectTransform>();
    }

    public void SetValue(int value)
    {
        Value = value;
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}