using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using ElectroPi.SupportTicket.Application.Tickets.Extensions;
using ElectroPi.SupportTicket.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed class GetTicketDetailsHandler(IAppDbContext db, ICurrentUser currentUser)
        : IRequestHandler<GetTicketDetailsQuery, TicketItemDto>
    {
        public async Task<TicketItemDto> Handle(
            GetTicketDetailsQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Ticket> query = db.Tickets
                .AsNoTracking();

            if(currentUser.CustomerId.HasValue)
            {
                query = query.Where(t => t.CustomerId == currentUser.CustomerId.Value);
            }

            Ticket? ticket = await query.FirstOrDefaultAsync(t => t.Id.Equals(request.TicketId), cancellationToken);

            if (ticket is null)
            {
                throw new KeyNotFoundException($"Ticket with id {request.TicketId} not found.");
            }

            PaginationResponse<TicketCommentDto> comments = await new GetTicketCommentsHandler(db)
                .Handle(new GetTicketCommentsQuery(request.TicketId, new Common.PaginationRequest()), cancellationToken);

            return ticket.Map(comments);
        }
    }
}
