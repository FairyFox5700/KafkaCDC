using KafkaCDC.Common.Entities;

namespace KafkaCDC.Deals.Domain
{
    public class Deal : BaseEntity
    {
        public string? ShortName { get; set; }
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

    public enum DealStatus
    {
        Open,
        Closed,
        Priced,
        Announced,
    }

    public enum DealType
    {
        Equity,
        Muni,
        FixedIncome,
    }
}
