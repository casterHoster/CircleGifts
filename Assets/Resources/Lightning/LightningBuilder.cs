using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LightningBuilder : MonoBehaviour
{
    [SerializeField] private Lightning _lightningPrefab;
    [SerializeField] private Extractor _extractor;

    private ObjectPool<Lightning> _lightningPool;
    private List<Vector3> _points;
    private List<Lightning> _lightnings;

    public void Initial()
    {
        _lightningPool = new ObjectPool<Lightning>(
            createFunc: () => Instantiate(_lightningPrefab),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            defaultCapacity: 25);

        _points = new List<Vector3>();
        _lightnings = new List<Lightning>();
        _extractor.CellAdded += GiveDirection;
        _extractor.Extracted += Realize;
        _extractor.PutOuted += Realize;
        _extractor.EndCellDifined += Realize;
    }

    private void OnDisable()
    {
        _extractor.CellAdded -= GiveDirection;
        _extractor.Extracted -= Realize;
        _extractor.PutOuted -= Realize;
        _extractor.EndCellDifined -= Realize;
    }

    private void GiveDirection(Cell cell)
    {
        _points.Add(cell.transform.position);

        if (_points.Count > 1)
        {
            var lightning = _lightningPool.Get();
            _lightnings.Add(lightning);
            lightning.DrawLightning(_points[_points.Count - 2], _points[_points.Count - 1]);
        }
    }

    private void Realize(Cell cell)
    {
        foreach (var lightning in _lightnings)
        {
            if (lightning.Target == cell.transform.position && lightning.gameObject.activeSelf == true)
            {
                _lightningPool.Release(lightning);
            }
        }

        _points.Remove(cell.transform.position);
    }

    private void Realize(Cell cell, int count)
    {
        foreach (var lightning in _lightnings)
        {
            if (lightning.Target == cell.transform.position && lightning.gameObject.activeSelf == true)
            {
                _lightningPool.Release(lightning);
            }
        }

        _points.Remove(cell.transform.position);
    }
}
