using KafkaCDC.Common.Entities;
using KafkaCDC.Traders.Data;

using Microsoft.EntityFrameworkCore;

namespace KafkaCDC.Traders.Domain
{
    public class TradersDbContext : DbContext
    {

        public TradersDbContext(DbContextOptions<TradersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trader> Traders => Set<Trader>();
        public DbSet<DealSubscriptions> DealSubscriptions => Set<DealSubscriptions>();
        public DbSet<Outbox> OutboxEvents => Set<Outbox>();
    }
}
