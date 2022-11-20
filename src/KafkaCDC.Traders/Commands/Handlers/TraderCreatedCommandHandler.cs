using System.Text.Json;

using KafkaCDC.Common.Entities;
using KafkaCDC.Traders.Data;
using KafkaCDC.Traders.Domain;

using MediatR;

namespace KafkaCDC.Traders.Commands.Handlers
{
    public class TraderCreatedCommandHandler : AsyncRequestHandler<TraderCreatedCommand>
    {
        private readonly TradersDbContext _dbContext;

        public TraderCreatedCommandHandler(TradersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(TraderCreatedCommand request,
            CancellationToken cancellationToken)
        {
            var trader = new Trader()
            {
                Id= Guid.NewGuid(),
                Address = request.Address,
                Email = request.Email,
                BirthDate = request.BirthDate,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
            };
            var outboxEvent = new Outbox
            {
                Id = Guid.NewGuid(),
                AggregateId = trader.Id,
                AggregateType = "traders",
                Type = "TraderAdded",
                Payload = JsonSerializer.Serialize(trader)
            };

            await _dbContext.Traders.AddAsync(trader, cancellationToken);

            await _dbContext.OutboxEvents.AddAsync(outboxEvent, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
