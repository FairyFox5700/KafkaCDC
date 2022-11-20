namespace KafkaCDC.Notifications.Events
{
    public record TraderCreatedEvent(
        Guid Id,
        string? Email,
        string? FirstName,
        string? LastName,
        string? Address,
        string? PhoneNumber,
        DateTime? BirthDate,
        string? Gender);
}
