namespace ElectroPi.SupportTicket.Domain.Events
{
    public class AgentAssignedEvent(
        Guid ticketId,
        Guid agentId,
        Guid assignedBy)
        : AbstractTicketDomainEvent(ticketId, assignedBy)
    {
        public Guid AgentId { get; private set; } = agentId;
    }
}
