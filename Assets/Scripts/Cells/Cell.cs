using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public abstract class Cell : MonoBehaviour
{
    public int Value {  get; protected set; }

    protected virtual void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    private void OnMouseOver()
    {
        
    }

    public RectTransform RectTransform { get; private set; }
}