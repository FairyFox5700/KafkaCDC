using MediatR;

namespace KafkaCDC.Traders.Commands
{
    public class TraderSubscriptionChangedCommand : IRequest
    {
        public Guid TraderSubscriptionId { get; set; }
        public Guid DealId { get; set; }
    }
}
