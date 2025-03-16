using UnityEngine;

public class ObjectScreenScaler : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        Scaler scaler = new Scaler();
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = new Vector3(_rectTransform.localScale.x * scaler.Scaling, _rectTransform.localScale.y * scaler.Scaling);
    }
}
