using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _moneyParticlePrefab;
    [SerializeField] private Extractor _extractor;

    private ParticleSystem _moneyParticle;

    private void OnEnable()
    {
        _extractor.EndCellDifined += PlayParticle;
        _moneyParticle = Instantiate(_moneyParticlePrefab);
    }

    private void PlayParticle(Cell cell)
    {
        _moneyParticle.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, -0.1f);
        _moneyParticle.Play();
    }
}
