namespace KafkaCDC.Traders.Events
{
    public record DealPriceChangedEvent(
        Guid Id,
        decimal? RevisedPriceRangeLow,
        decimal? RevisedPriceRangeHigh)
    {
    }
}
