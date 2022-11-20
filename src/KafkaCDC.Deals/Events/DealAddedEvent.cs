using KafkaCDC.Deals.Domain;

namespace KafkaCDC.Deals.Events
{
    public record DealAddedEvent(string ShortName, 
        DealType? dealType, 
        DealStatus? dealStatus, 
        DateTime createdTimeStamp,
        DateTime lastModifiedTimeStamp,
        decimal? amount,
        decimal? initialPriceRangeLow,
        decimal? initialPriceRangeHigh,
        decimal? revisedPriceRangeLow,
        decimal? revisedPriceRangeHigh)
    {
    }
}
