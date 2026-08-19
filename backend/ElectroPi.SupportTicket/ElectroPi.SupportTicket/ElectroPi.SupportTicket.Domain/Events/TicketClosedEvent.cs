namespace ElectroPi.SupportTicket.Domain.Events
{
    public class TicketClosedEvent(Guid ticketId, Guid closedBy)
        : AbstractTicketDomainEvent(ticketId, closedBy)
    {
    }
}
