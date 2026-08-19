using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed class GetTicketCommentsHandler(IAppDbContext db)
        : IRequestHandler<GetTicketCommentsQuery, PaginationResponse<TicketCommentDto>>
    {
        public async Task<PaginationResponse<TicketCommentDto>> Handle(
            GetTicketCommentsQuery request,
            CancellationToken cancellationToken)
        {
            var query = db.Comments
                .AsNoTracking()
                .Where(x => x.TicketId == request.TicketId)
                .OrderByDescending(x=>x.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip(request.Pagination.Skip)
                .Take(request.Pagination.PageSize)
                .Select(x => new TicketCommentDto(
                    x.Id,
                    x.Content,
                    x.CreatedBy.Value,
                    x.CreatedAt))
                .ToListAsync(cancellationToken);

            return new PaginationResponse<TicketCommentDto>(
                items,
                request.Pagination.PageNumber,
                request.Pagination.PageSize,
                totalCount);
        }
    }
}
