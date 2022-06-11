namespace KafkaCDC.Deals.Events
{
    public record DealUpdatedEvent(
        Guid Id,
        decimal? RevisedPriceRangeLow,
        decimal? RevisedPriceRangeHigh)
    {
    }
}
