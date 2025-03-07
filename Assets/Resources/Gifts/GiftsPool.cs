using UnityEngine;
using UnityEngine.Pool;

public class GiftsPool : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private GiftsFabric _giftFabric;
    [SerializeField] private CellsCreator _cellsCreator;
    [SerializeField] private Extractor _extractor;
    [SerializeField] private WrapperViewer _wrapperViewer;

    private ObjectPool<Gift> _giftPool;
    private float _collerationScalingCell = 4;

    public void Initial()
    {
        _giftPool = new ObjectPool<Gift>
            (
            createFunc: () => _giftFabric.InstantiateRandomGift(_board.RectTransform),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            defaultCapacity: 25
            );

        _extractor.Extracted += ReleaseFromCell;
        _cellsCreator.CellCreated += GenerateForCell;
        _giftFabric.Maximised += ReleaseFromCell;
        _wrapperViewer.ValueChanged += RealeseFromUI;
    }

    private void OnDisable()
    {
        _cellsCreator.CellCreated -= GenerateForCell;
        _extractor.Extracted -= ReleaseFromCell;
        _giftFabric.Maximised -= ReleaseFromCell;
        _wrapperViewer.ValueChanged += RealeseFromUI;
    }

    public void GenerateForCell(Cell cell)
    {
        var gift = _giftPool.Get();
        cell.Fill(gift);
        _giftFabric.RandomCharacterize(gift);
        gift.transform.localScale = cell.transform.localScale / _collerationScalingCell;
        gift.gameObject.SetActive(true);
        gift.gameObject.transform.position =
            new Vector3(cell.transform.position.x, cell.transform.position.y, gift.transform.position.z);
    }

    public Gift GenerateForCanvas(int value, Transform transform, Cell cell)
    {
        var gift = _giftPool.Get();
        _giftFabric.TargetingCharacterize(gift, value);
        gift.transform.localScale = cell.transform.localScale / _collerationScalingCell;
        gift.gameObject.SetActive(true);
        gift.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, gift.gameObject.transform.position.z);
        return gift;
    }

    private void ReleaseFromCell(Cell cell)
    {
        _giftPool.Release(cell.Gift);
        cell.Clear();
    }

    private void RealeseFromUI(Gift gift)
    {
        _giftPool.Release(gift);
    }
}
