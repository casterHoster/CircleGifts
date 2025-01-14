
public delegate void ShowCell(float x, float y, float gift);

public class Lines
{
    public const int Size = 5;

    private ShowCell _cell;

    public Lines(ShowCell cell)
    {
        _cell = cell;
    }

    public void Start()
    {

    }

    public void Click(float x, float y)
    {

    }
}
