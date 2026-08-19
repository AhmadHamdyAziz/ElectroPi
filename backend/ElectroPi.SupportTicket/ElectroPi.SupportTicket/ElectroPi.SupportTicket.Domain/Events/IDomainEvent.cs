namespace ElectroPi.SupportTicket.Domain.Events
{
    public interface IDomainEvent
    {
        DateTimeOffset OccurredAt { get; }
    }
}
