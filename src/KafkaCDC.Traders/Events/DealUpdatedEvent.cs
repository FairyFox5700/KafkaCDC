namespace KafkaCDC.Traders.Events
{
    public record DealUpdatedEvent(
        Guid Id,
        decimal? RevisedPriceRangeLow,
        decimal? RevisedPriceRangeHigh)
    {
    }
}
