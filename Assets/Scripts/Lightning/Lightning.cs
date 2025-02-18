using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class Lightning : MonoBehaviour
{
    [SerializeField] private int _segments = 10;
    [SerializeField] private float _startLineWidth = 1f;
    [SerializeField] private float _endLineWidth = 1f;

    private LineRenderer _lineRenderer;

    public Vector3 Target {  get; private set; }

    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segments;
        _lineRenderer.startWidth = _startLineWidth;
        _lineRenderer.endWidth = _endLineWidth;
    }

    public void DrawLightning(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3[] positions = new Vector3[_segments];
        positions[0] = startPoint;
        positions[_segments - 1] = endPoint;

        for (int i = 1; i < _segments - 1; i++)
        {
            positions[i] = Vector3.Lerp(startPoint, endPoint, (float)i / (_segments - 1));
        }

        _lineRenderer.SetPositions(positions);
        Target = endPoint;
    }
}