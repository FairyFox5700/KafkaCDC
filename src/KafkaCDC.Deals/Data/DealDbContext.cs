using System.Data;
using KafkaCDC.Common.Entities;
using KafkaCDC.Deals.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KafkaCDC.Deals.Data
{
    public class DealDbContext : DbContext
    {
        public DealDbContext(DbContextOptions<DealDbContext> options)
            : base(options)
        {
        }
        public DbSet<Deal> Deals => Set<Deal>();
        public DbSet<Outbox> OutboxEvents => Set<Outbox>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Deal>()
                .Property(d => d.DealStatus)
                .HasConversion(new EnumToStringConverter<DealStatus>());

            modelBuilder
                .Entity<Deal>()
                .Property(d => d.DealType)
                .HasConversion(new EnumToStringConverter<DealType>());
        }
    }
}
