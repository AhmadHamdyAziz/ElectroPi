using ElectroPi.SupportTicket.Domain.Enums;

namespace ElectroPi.SupportTicket.Domain.Events
{
    public class TicketPriorityChangedEvent(
        Guid ticketId,
        TicketPriority newPriority, 
        Guid prioritizedBy) 
        : AbstractTicketDomainEvent(ticketId, prioritizedBy)
    {
        public TicketPriority NewPriority { get; private set; } = newPriority;
    }
}
