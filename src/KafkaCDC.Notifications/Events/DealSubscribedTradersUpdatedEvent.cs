namespace KafkaCDC.Notifications.Events
{
    public record DealSubscribedTradersUpdatedEvent(List<string> EmailsList,
        Guid DealId,
        decimal? RevisedPriceRangeLow,
        decimal? RevisedPriceRangeHigh)
    {
    }
}
