using System.Text.Json;

using KafkaCDC.Common.Entities;
using KafkaCDC.Traders.Domain;
using KafkaCDC.Traders.Events;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace KafkaCDC.Traders.Commands.Handlers
{
    public class TraderSubscriptionChangedHandler : AsyncRequestHandler<TraderSubscriptionChangedCommand>
    {
        private readonly TradersDbContext _dbContext;

        public TraderSubscriptionChangedHandler(TradersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override async Task Handle(TraderSubscriptionChangedCommand request,
            CancellationToken cancellationToken)
        {
            var dealSubscription = await _dbContext.DealSubscriptions
                .FirstOrDefaultAsync(s => s.Id == request.TraderSubscriptionId,
                    cancellationToken: cancellationToken);
            if (dealSubscription == null)
                throw new ApplicationException("Deal subscription is not found.");

            dealSubscription.DealId = request.DealId;
            var outboxEvent = new Outbox
            {
                Id = Guid.NewGuid(),
                AggregateId = request.TraderSubscriptionId,
                AggregateType = "DealSubscriptions",
                Type = "TraderSubscriptionUpdated",
                //Payload = JsonSerializer.Serialize(new TraderSubscriptionChangedEvent
                //{
                //    DealId = request.DealId,
                //    TraderSubscriptionId = request.TraderSubscriptionId,
                //})
            };

            _dbContext.DealSubscriptions.Update(dealSubscription);

            _dbContext.OutboxEvents.Add(outboxEvent);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
