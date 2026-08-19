using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using MediatR;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed record GetTicketCommentsQuery(
        Guid TicketId,
        PaginationRequest Pagination)
        : IRequest<PaginationResponse<TicketCommentDto>>;
}
