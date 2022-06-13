using System.Text.Json;

using KafkaCDC.Common.Entities;
using KafkaCDC.Deals.Data;
using KafkaCDC.Deals.Domain;
using KafkaCDC.Deals.Events;

using MediatR;


namespace KafkaCDC.Deals.Comands.Handlers
{
    public class UpdateDealPriceCommandHandler : AsyncRequestHandler<UpdateDealPriceCommand>
    {

        private readonly DealDbContext _dbContext;

        public UpdateDealPriceCommandHandler(DealDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(UpdateDealPriceCommand request, CancellationToken cancellationToken)
        {
            var deal = await _dbContext.Deals.FindAsync(request.Id);
            if (deal == null)
                throw new ApplicationException("Deal was not found.");

            deal.RevisedPriceRangeLow = request.PriceLow;
            deal.RevisedPriceRangeHigh = request.PriceHigh;

            var outboxEvent = new Outbox
            {
                Id = Guid.NewGuid(),
                AggregateId = deal.Id,
                AggregateType = "deals",
                Type = "DealUpdated",
                Payload = JsonSerializer.Serialize(new DealUpdatedEvent(
                    deal.Id,
                    deal.RevisedPriceRangeLow,
                    deal.RevisedPriceRangeHigh)),
            };

            _dbContext.Deals.Update(deal);

            _dbContext.OutboxEvents.Add(outboxEvent);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
