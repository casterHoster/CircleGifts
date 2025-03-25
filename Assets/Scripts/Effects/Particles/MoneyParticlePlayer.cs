using UnityEngine;
using Cells;
using Gameplay;

namespace Effects
{
    public class MoneyParticlePlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _moneyParticlePrefab;
        [SerializeField] private Extractor _extractor;

        private ParticleSystem _moneyParticle;

        public void Initial()
        {
            _extractor.EndCellDifined += PlayParticle;
            _moneyParticle = Instantiate(_moneyParticlePrefab);
        }

        private void OnDisable()
        {
            _extractor.EndCellDifined -= PlayParticle;
        }

        private void PlayParticle(Cell cell, int count)
        {
            _moneyParticle.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, -0.1f);
            _moneyParticle.Play();
        }
    }
}
