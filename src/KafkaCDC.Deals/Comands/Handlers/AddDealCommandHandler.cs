using System.Text.Json;

using KafkaCDC.Common.Entities;
using KafkaCDC.Deals.Data;
using KafkaCDC.Deals.Domain;
using KafkaCDC.Deals.Events;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace KafkaCDC.Deals.Comands.Handlers
{
    public class AddDealCommandHandler : AsyncRequestHandler<AddDealCommand>
    {
        private readonly DealDbContext _dealDbContext;

        public AddDealCommandHandler(DealDbContext dealDbContext)
        {
            _dealDbContext = dealDbContext;
        }
        protected override async Task Handle(AddDealCommand request, CancellationToken cancellationToken)
        {
            if (await _dealDbContext.Deals.AsNoTracking()
                    .AnyAsync(d => d.ShortName == request.ShortName, cancellationToken: cancellationToken))
                throw new ApplicationException("Deal is already exist.");

            var deal = new Deal()
            {
                DealStatus = request.DealStatus,
                ShortName = request.ShortName,
                DealType = request.DealType,
                Amount = request.Amount,
                InitialPriceRangeHigh = request.InitialPriceRangeHigh,
                InitialPriceRangeLow = request.InitialPriceRangeLow,
                CreatedTimeStamp = DateTime.UtcNow,
                LastModifiedTimeStamp = DateTime.UtcNow,
                RevisedPriceRangeHigh = request.RevisedPriceRangeHigh,
                RevisedPriceRangeLow = request.RevisedPriceRangeLow,
                Id = Guid.NewGuid(),
            };

            var all = await _dealDbContext.OutboxEvents.ToListAsync();
            var outboxEvent = new Outbox
            {
                Id = Guid.NewGuid(),
                AggregateId = deal.Id,
                AggregateType = "deals",
                Type = "DealAdded",
                Payload = JsonSerializer.Serialize(new DealAddedEvent(
                    deal.ShortName,
                    deal.DealType,
                    deal.DealStatus,
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    deal.Amount,
                    deal.InitialPriceRangeLow,
                    deal.InitialPriceRangeHigh,
                    deal.RevisedPriceRangeLow,
                    deal.RevisedPriceRangeHigh))
            };

            await _dealDbContext.Deals.AddAsync(deal, cancellationToken);
            await _dealDbContext.OutboxEvents.AddAsync(outboxEvent, cancellationToken);

            await _dealDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
