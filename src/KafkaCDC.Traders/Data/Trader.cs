using KafkaCDC.Common.Entities;

namespace KafkaCDC.Traders.Data
{
    public class Trader : BaseEntity
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }

    }

    public class DealSubscriptions : BaseEntity
    {
        public Guid TraderId { get; set; }
        public Guid DealId { get; set; }
    }
}
