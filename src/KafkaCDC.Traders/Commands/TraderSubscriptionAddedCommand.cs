using MediatR;

namespace KafkaCDC.Traders.Commands
{
    public class TraderSubscriptionAddedCommand : IRequest
    {
        public Guid TraderId { get; set; }
        public Guid DealId { get; set; }
    }
}
