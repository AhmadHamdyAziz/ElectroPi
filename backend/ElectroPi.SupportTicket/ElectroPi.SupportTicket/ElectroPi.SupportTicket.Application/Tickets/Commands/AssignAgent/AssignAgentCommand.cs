using MediatR;

namespace ElectroPi.SupportTicket.Application.Tickets.Commands.AssignAgent
{
    public sealed record AssignAgentCommand(
        Guid TicketId,
        Guid AgentId) : IRequest;
}
