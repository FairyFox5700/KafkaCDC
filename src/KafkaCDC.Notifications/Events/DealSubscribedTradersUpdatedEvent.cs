namespace KafkaCDC.Notifications.Events
{
    public class DealSubscribedTradersUpdatedEvent
    {
        public List<string> EmailsList { get; set; }
        public Guid DealId { get; set; }

        public decimal? RevisedPriceRangeLow { get; set; }
        public decimal? RevisedPriceRangeHigh { get; set; }
    }
}
