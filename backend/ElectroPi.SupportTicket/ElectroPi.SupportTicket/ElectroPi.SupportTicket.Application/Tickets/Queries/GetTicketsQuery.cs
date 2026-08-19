using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using MediatR;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed record GetTicketsQuery(
    PaginationRequest Pagination,
    TicketFilter? Filter = null,
    TicketSortOption? Sort = null)
        : IRequest<PaginationResponse<TicketListItemDto>>;
}
