using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Board : MonoBehaviour
{
    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
}
