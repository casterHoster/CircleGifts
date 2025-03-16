public class Task
{
    public int GiftValue { get; private set; }

    public int GiftsCount { get; private set; }

    public int Moves { get; private set; }

    public Task(int giftValue, int giftsCount, int movesNumber)
    {
        GiftValue = giftValue;
        GiftsCount = giftsCount;
        Moves = movesNumber;
    }
}
