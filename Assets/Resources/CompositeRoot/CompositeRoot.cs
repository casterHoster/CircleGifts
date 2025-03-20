using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private GiftsGenerator _giftsPool;
    [SerializeField] private GiftsFabric _giftsFabric;
    [SerializeField] private NeighboursSearcher _neighboursSearcher;
    [SerializeField] private Extractor _extractor;
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private FieldUpdater _operator;
    [SerializeField] private CellsSearcher _cellsSearcher;
    [SerializeField] private LightningBuilder _lightningBuilder;
    [SerializeField] private GameFinisher _gameFinisher;
    [SerializeField] private CellsLighter _lighterCells;
    [SerializeField] private MoneyParticlePlayer _moneyParticlePlayer;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Wrapper _wrapper;
    [SerializeField] private CellsColliderToggle _cellsColliderToggle;
    [SerializeField] private BackgroundSoundsRegulator _backgroundSoundsRegulator;
    [SerializeField] private EffectsSoundsRegulator _effectsSoundsRegulator;
    [SerializeField] private VolumeSettings _volumeSettings;
    [SerializeField] private PathPointer _pathPointer;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private TaskRegulator _taskRegulator;
    [SerializeField] private Reward _reward;

    private void Start()
    {
        _extractor.Initial();
        _giftsFabric.Initial();
        _neighboursSearcher.Initial();
        _pathPointer.Initial();
        _lighterCells.Initial();
        _giftsPool.Initial();
        _operator.Initial();
        _cellsSearcher.Initial();
        _cellsColliderToggle.Initial();
        _taskRegulator.Initial();
        _cellsCreator.Initial();
        _lightningBuilder.Initial();
        _gameFinisher.Initial();
        _moneyParticlePlayer.Initial();
        _scoreCounter.Initial();
        _wrapper.Initial();
        _backgroundSoundsRegulator.Initial();
        _effectsSoundsRegulator.Initial();
        _volumeSettings.Initial();
        _leaderboard.Initial();
        //_reward.Initial();
    }
}
