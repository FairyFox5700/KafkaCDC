namespace KafkaCDC.Common.Entities
{
    public class Outbox : BaseEntity
    {
        public string? AggregateType { get; set; }

        public Guid AggregateId { get; set; }

        public string? Type { get; set; }

        public string? Payload { get; set; }

        public DateTime DateOccurred { get; set; } = DateTime.UtcNow;
    }
}
