using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(SpriteRenderer))]
public class Gift : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _speed = 2;
    float threshold = 0.1f;

    public int Value { get; protected set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetValue(int value)
    {
        Value = value;
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public IEnumerator Move(Cell cell)
    {
        while (Vector3.Distance(transform.position, cell.transform.position) > threshold)
        {
            Vector3 targetPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, transform.position.z);

        if (GetComponent<BoxCollider2D>().enabled == false)
            GetComponent<BoxCollider2D>().enabled = true;
    }
}