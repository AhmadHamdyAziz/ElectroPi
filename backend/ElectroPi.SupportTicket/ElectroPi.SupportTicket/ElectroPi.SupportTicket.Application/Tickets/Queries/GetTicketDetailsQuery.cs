using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using MediatR;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed record GetTicketDetailsQuery(Guid TicketId)
        : IRequest<TicketItemDto>;
}
