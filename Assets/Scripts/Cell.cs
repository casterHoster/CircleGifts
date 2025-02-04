using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Cell : MonoBehaviour
{
    private bool _isFilled;

    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
}
