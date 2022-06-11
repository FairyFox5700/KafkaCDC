using KafkaCDC.Deals.Domain;

using MediatR;

namespace KafkaCDC.Deals.Comands
{
    public class AddDealCommand : IRequest
    {
        public string? ShortName { get; set; }
        public DealType? DealType { get; set; }

        public DealStatus? DealStatus { get; set; }
        public decimal? Amount { get; set; }

        public decimal? InitialPriceRangeLow { get; set; }

        public decimal? InitialPriceRangeHigh { get; set; }

        public decimal? RevisedPriceRangeLow { get; set; }

        public decimal? RevisedPriceRangeHigh { get; set; }
    }
}
