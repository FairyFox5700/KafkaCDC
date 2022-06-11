using KafkaCDC.Deals.Domain;

namespace KafkaCDC.Deals.Events
{
    public class DealAddedEvent
    {
        public DealAddedEvent(string shortName, DealType? dealType, DealStatus? dealStatus, DateTime createdTimeStamp, DateTime lastModifiedTimeStamp, decimal? amount, decimal? initialPriceRangeLow, decimal? initialPriceRangeHigh, decimal? revisedPriceRangeLow, decimal? revisedPriceRangeHigh)
        {
            ShortName = shortName;
            DealType = dealType;
            DealStatus = dealStatus;
            CreatedTimeStamp = createdTimeStamp;
            LastModifiedTimeStamp = lastModifiedTimeStamp;
            Amount = amount;
            InitialPriceRangeLow = initialPriceRangeLow;
            InitialPriceRangeHigh = initialPriceRangeHigh;
            RevisedPriceRangeLow = revisedPriceRangeLow;
            RevisedPriceRangeHigh = revisedPriceRangeHigh;
        }

        public string ShortName { get; set; }
        public DealType? DealType { get; set; }

        public DealStatus? DealStatus { get; set; }

        public DateTime CreatedTimeStamp { get; set; }

        public DateTime LastModifiedTimeStamp { get; set; }

        public decimal? Amount { get; set; }

        public decimal? InitialPriceRangeLow { get; set; }

        public decimal? InitialPriceRangeHigh { get; set; }

        public decimal? RevisedPriceRangeLow { get; set; }

        public decimal? RevisedPriceRangeHigh { get; set; }
    }
}
