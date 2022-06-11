
using MediatR;

namespace KafkaCDC.Deals.Comands
{
    public class UpdateDealPriceCommand : IRequest
    {
        public Guid Id { get; set; }
        public decimal? PriceLow { get; set; }

        public decimal? PriceHigh { get; set; }
    }
}
