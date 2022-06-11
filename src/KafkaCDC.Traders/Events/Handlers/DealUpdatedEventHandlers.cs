using System.Text.Json;

using KafkaCDC.Common.Entities;
using KafkaCDC.Common.Kafka;
using KafkaCDC.Traders.Domain;

using Microsoft.EntityFrameworkCore;

namespace KafkaCDC.Traders.Events.Handlers
{
    public class DealUpdatedEventHandlers : IKafkaHandler<string, DealUpdatedEvent>
    {

        private readonly TradersDbContext _dbContext;

        public DealUpdatedEventHandlers(TradersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(string key, DealUpdatedEvent value)
        {
            var subscribedTraderEmails = await (from subscription in _dbContext.DealSubscriptions
                                                join trader in _dbContext.Traders
                                                    on subscription.TraderId equals trader.Id
                                                where subscription.DealId == value.Id
                                                select trader.Email).ToListAsync();

            var outboxEvent = new Outbox
            {
                Id = Guid.NewGuid(),
                AggregateId = value.Id,
                AggregateType = "subscriptions",
                Type = "DealSubscriptionUpdated",
                Payload = JsonSerializer.Serialize(new DealSubscribedTradersUpdatedEvent
                {
                    RevisedPriceRangeHigh = value.RevisedPriceRangeHigh,
                    DealId = value.Id,
                    RevisedPriceRangeLow = value.RevisedPriceRangeLow,
                    EmailsList = subscribedTraderEmails.ToList(),
                })
            };

            await _dbContext.OutboxEvents.AddAsync(outboxEvent);

            await _dbContext.SaveChangesAsync();
        }
    }
}
