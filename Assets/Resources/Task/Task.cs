
public class Task
{
    public float GiftValue { get; private set; }

    public float GiftsCount { get; private set; }

    public float MovesNumber { get; private set; }

    public Task(float giftValue, float giftsCount, float movesNumber)
    {
        GiftValue = giftValue;
        GiftsCount = giftsCount;
        MovesNumber = movesNumber;
    }
}
