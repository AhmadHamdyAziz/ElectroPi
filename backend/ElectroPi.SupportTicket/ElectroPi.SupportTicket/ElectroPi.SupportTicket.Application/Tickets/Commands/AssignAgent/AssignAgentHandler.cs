using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Domain.Constants;
using ElectroPi.SupportTicket.Domain.Factories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Tickets.Commands.AssignAgent
{
    public sealed class AssignAgentHandler(
        IAppDbContext db,
        ITicketStateFactory stateFactory) : IRequestHandler<AssignAgentCommand>
    {
        public async Task Handle(
            AssignAgentCommand request,
            CancellationToken cancellationToken)
        {
            var ticket = await db.Tickets
                .SingleOrDefaultAsync(
                    x => x.Id == request.TicketId,
                    cancellationToken);

            if (ticket is null)
                throw new KeyNotFoundException(
                    $"Ticket '{request.TicketId}' was not found.");

            var agent = await db.Users
                .AsNoTracking()
                .Include(u=>u.Role)
                .SingleOrDefaultAsync(u=>u.Id == request.AgentId, cancellationToken);

            if (agent is null)
            {
                throw new KeyNotFoundException("Agent not found.");
            }

            if (!agent.Role.Name.Equals(RoleNames.Agent))
            {
                throw new ValidationException("The selected user is not an agent.");
            }

            var state = stateFactory.Create(ticket.Status);

            ticket.AssignAgent(state, request.AgentId, request.AgentId);

            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
