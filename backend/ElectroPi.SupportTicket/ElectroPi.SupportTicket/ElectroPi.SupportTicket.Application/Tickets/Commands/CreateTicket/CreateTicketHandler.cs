using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Domain.Entities;
using ElectroPi.SupportTicket.Domain.Enums;
using MediatR;


namespace ElectroPi.SupportTicket.Application.Tickets.Commands.CreateTicket
{
    public sealed class CreateTicketHandler(
        IAppDbContext db,
        ICurrentUser currentUser)
                : IRequestHandler<CreateTicketCommand, Guid>
    {
        public async Task<Guid> Handle(
            CreateTicketCommand request,
            CancellationToken cancellationToken)
        {
            if (!currentUser.IsAuthenticated || 
                !currentUser.UserId.HasValue ||
                !currentUser.CustomerId.HasValue)
                throw new UnauthorizedAccessException();

            var ticket = Ticket.Create(
                request.Title,
                request.Description,
                request.Priority,
                currentUser.CustomerId.Value,
                currentUser.UserId.Value);

            db.Tickets.Add(ticket);

            await db.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }
    }
}
