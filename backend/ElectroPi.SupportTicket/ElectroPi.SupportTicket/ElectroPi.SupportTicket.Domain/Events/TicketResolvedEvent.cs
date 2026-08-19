namespace ElectroPi.SupportTicket.Domain.Events
{
    public class TicketResolvedEvent(Guid ticketId, Guid resolvedBy)
        : AbstractTicketDomainEvent(ticketId, resolvedBy)
    {
    }
}
