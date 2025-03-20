public class TargetTask
{
    public TargetTask(int giftValue, int giftsCount, int movesNumber)
    {
        GiftValue = giftValue;
        GiftsCount = giftsCount;
        Moves = movesNumber;
    }

    public int GiftValue { get; private set; }

    public int GiftsCount { get; private set; }

    public int Moves { get; private set; }
}
