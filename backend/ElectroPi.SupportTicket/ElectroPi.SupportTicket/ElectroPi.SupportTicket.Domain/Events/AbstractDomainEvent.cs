namespace ElectroPi.SupportTicket.Domain.Events
{
    public abstract class AbstractDomainEvent : IDomainEvent
    {
        public DateTimeOffset OccurredAt { get; private set; }
        protected AbstractDomainEvent()
        {
            OccurredAt = DateTimeOffset.UtcNow;
        }
    }
}
