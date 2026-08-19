using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using MediatR;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed record GetTicketActivitiesQuery(
    PaginationRequest Pagination)
        : IRequest<PaginationResponse<TicketActivityDto>>;
}
