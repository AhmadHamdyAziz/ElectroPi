using ElectroPi.SupportTicket.Domain.Enums;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed record TicketFilter(
    string? Search = null,
    TicketState? Status = null,
    TicketPriority? Priority = null,
    Guid? CustomerId = null,
    Guid? AssignedAgentId = null);
}
