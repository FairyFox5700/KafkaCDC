namespace KafkaCDC.Common.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = new();
    }
}
