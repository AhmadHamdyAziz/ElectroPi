using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Domain.Enums;

namespace ElectroPi.SupportTicket.Application.Tickets.DTOs
{
    public sealed record TicketListItemDto(
        Guid Id,
        string Title,
        TicketState Status,
        TicketPriority Priority,
        Guid CustomerId,
        Guid? AssignedAgentId,
        DateTimeOffset CreatedAt);
    public sealed record TicketItemDto(
        Guid Id,
        string Title,
        string Description,
        TicketState Status,
        TicketPriority Priority,
        Guid CustomerId,
        string CustomerName,
        Guid? AssignedAgentId,
        string AssignedAgent,
        DateTimeOffset CreatedAt,
        PaginationResponse<TicketCommentDto> Comments);
}
