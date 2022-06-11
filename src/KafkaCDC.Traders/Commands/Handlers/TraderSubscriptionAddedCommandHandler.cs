using System.Text.Json;

using KafkaCDC.Common.Entities;
using KafkaCDC.Traders.Data;
using KafkaCDC.Traders.Domain;

using MediatR;

namespace KafkaCDC.Traders.Commands.Handlers
{
    public class TraderSubscriptionAddedCommandHandler : AsyncRequestHandler<TraderSubscriptionAddedCommand>
    {
        private readonly TradersDbContext _dbContext;

        public TraderSubscriptionAddedCommandHandler(TradersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override async Task Handle(TraderSubscriptionAddedCommand request,
            CancellationToken cancellationToken)
        {
            var dealSubscription = new DealSubscriptions()
            {
                DealId = request.DealId,
                TraderId = request.TraderId,
            };
            var outboxEvent = new Outbox
            {
                Id = Guid.NewGuid(),
                AggregateId = request.DealId,
                AggregateType = "DealSubscriptions",
                Type = "TraderSubscriptionAdded",
                Payload = JsonSerializer.Serialize(dealSubscription)
            };

            await _dbContext.DealSubscriptions.AddAsync(dealSubscription, cancellationToken);

            await _dbContext.OutboxEvents.AddAsync(outboxEvent, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
