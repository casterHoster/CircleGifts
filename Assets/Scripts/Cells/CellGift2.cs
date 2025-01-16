
public class CellGift2 : Cell
{
    private int _value = 2;

    protected override void Awake()
    {
        base.Awake();
        Value = _value;
    }
}
