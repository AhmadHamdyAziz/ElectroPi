using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Domain.Entities;
using ElectroPi.SupportTicket.Domain.Factories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Tickets.Commands.AddComment
{
    public sealed class AddCommentCommandHandler(
        IAppDbContext db,
        ICurrentUser currentUser,
        ITicketStateFactory stateFactory)
        : IRequestHandler<AddCommentCommand>
    {
        public async Task Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            Ticket? ticket = await db.Tickets.Include(t=>t.Comments).FirstOrDefaultAsync(t => t.Id == request.TicketId, cancellationToken);

            if (ticket is null)
            {
                throw new InvalidOperationException($"Ticket with ID {request.TicketId} not found.");
            }

            var state = stateFactory.Create(ticket.Status);

            var initialCommentCount = ticket.Comments.Count;
            ticket.AddComment(state, request.Comment);

            var newComments = ticket.Comments.Skip(initialCommentCount);
            db.Comments.AddRange(newComments);

            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
