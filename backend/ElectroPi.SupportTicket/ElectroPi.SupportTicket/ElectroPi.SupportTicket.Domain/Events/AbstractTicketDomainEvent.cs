namespace ElectroPi.SupportTicket.Domain.Events
{
    public abstract class AbstractTicketDomainEvent(
        Guid ticketId,
        Guid actorId) : AbstractDomainEvent
    {
        public Guid TicketId { get; } = ticketId;

        public Guid ActorId { get; } = actorId;
    }
}
