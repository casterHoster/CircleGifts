public class TargetTask
{
    public int GiftValue { get; private set; }

    public int GiftsCount { get; private set; }

    public int Moves { get; private set; }

    public TargetTask(int giftValue, int giftsCount, int movesNumber)
    {
        GiftValue = giftValue;
        GiftsCount = giftsCount;
        Moves = movesNumber;
    }
}
