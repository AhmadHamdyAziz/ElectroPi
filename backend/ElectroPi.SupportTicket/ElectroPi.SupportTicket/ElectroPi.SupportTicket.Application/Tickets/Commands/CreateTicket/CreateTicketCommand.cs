using ElectroPi.SupportTicket.Domain.Enums;
using MediatR;


namespace ElectroPi.SupportTicket.Application.Tickets.Commands.CreateTicket
{
    public sealed record CreateTicketCommand(
        string Title,
        string Description,
        TicketPriority Priority)
        : IRequest<Guid>;
}
