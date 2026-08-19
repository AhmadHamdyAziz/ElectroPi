using ElectroPi.SupportTicket.Domain.Enums;

namespace ElectroPi.SupportTicket.Domain.Events
{
    public class TicketCreatedEvent(
        Guid ticketId,
        string title,
        string description,
        TicketPriority priority,
        Guid customerId,
        Guid actorId)
        : AbstractTicketDomainEvent(ticketId, actorId)
    {
        public string Title { get; private set; } = title;
        public string Description { get; private set; } = description;
        public TicketPriority Priority { get; private set; } = priority;
        public Guid CustomerId { get; private set; } = customerId;
    }
}
