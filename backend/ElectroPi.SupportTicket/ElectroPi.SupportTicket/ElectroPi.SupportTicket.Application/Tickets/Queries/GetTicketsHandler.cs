using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using ElectroPi.SupportTicket.Domain.Constants;
using ElectroPi.SupportTicket.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed class GetTicketsHandler(IAppDbContext db, ICurrentUser currentUser)
        : IRequestHandler<GetTicketsQuery, PaginationResponse<TicketListItemDto>>
    {
        public async Task<PaginationResponse<TicketListItemDto>> Handle(
            GetTicketsQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Ticket> query = db.Tickets
                .AsNoTracking();

            query = ApplyFilter(query, request.Filter);
            query = ApplySort(query, request.Sort);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip(request.Pagination.Skip)
                .Take(request.Pagination.PageSize)
                .Select(x => new TicketListItemDto(
                    x.Id,
                    x.Title,
                    x.Status,
                    x.Priority,
                    x.CustomerId,
                    x.AssignedAgentId,
                    x.CreatedAt))
                .ToListAsync(cancellationToken);

            return new PaginationResponse<TicketListItemDto>(
                items,
                request.Pagination.PageNumber,
                request.Pagination.PageSize,
                totalCount);
        }

        private IQueryable<Ticket> ApplyFilter(
            IQueryable<Ticket> query,
            TicketFilter? filter)
        {
            if (currentUser.RoleName == RoleNames.Agent)
            {
                query = query.Where(x =>
                    x.AssignedAgentId == currentUser.UserId);
            }

            if (currentUser.CustomerId.HasValue)
            {
                query = query.Where(x =>
                    x.CustomerId == currentUser.CustomerId.Value);
            }

            if (filter is null)
                return query;

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.Trim();

                query = query.Where(x =>
                    x.Title.Contains(search) ||
                    x.Description.Contains(search));
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == filter.Status.Value);
            }

            if (filter.Priority.HasValue)
            {
                query = query.Where(x =>
                    x.Priority == filter.Priority.Value);
            }

            if (!currentUser.CustomerId.HasValue && filter.CustomerId.HasValue)
            {
                query = query.Where(x =>
                    x.CustomerId == filter.CustomerId.Value);
            }

            if (filter.AssignedAgentId.HasValue)
            {
                query = query.Where(x =>
                    x.AssignedAgentId == filter.AssignedAgentId.Value);
            }

            return query;
        }

        private IQueryable<Ticket> ApplySort(
            IQueryable<Ticket> query,
            TicketSortOption? sort)
        {
            sort ??= new TicketSortOption();

            return sort.SortField switch
            {
                TicketSortField.Title =>
                    sort.SortDirection == SortDirection.Ascending
                        ? query
                            .OrderBy(x => x.Title)
                            .ThenBy(x => x.Id)
                        : query
                            .OrderByDescending(x => x.Title)
                            .ThenByDescending(x => x.Id),

                TicketSortField.Priority =>
                    sort.SortDirection == SortDirection.Ascending
                        ? query
                            .OrderBy(x => x.Priority)
                            .ThenBy(x => x.Id)
                        : query
                            .OrderByDescending(x => x.Priority)
                            .ThenByDescending(x => x.Id),

                TicketSortField.Status =>
                    sort.SortDirection == SortDirection.Ascending
                        ? query
                            .OrderBy(x => x.Status)
                            .ThenBy(x => x.Id)
                        : query
                            .OrderByDescending(x => x.Status)
                            .ThenByDescending(x => x.Id),

                TicketSortField.CreatedAt =>
                    sort.SortDirection == SortDirection.Ascending
                        ? query
                            .OrderBy(x => x.CreatedAt)
                            .ThenBy(x => x.Id)
                        : query
                            .OrderByDescending(x => x.CreatedAt)
                            .ThenByDescending(x => x.Id),

                _ => query
                        .OrderByDescending(x => x.CreatedAt)
                        .ThenByDescending(x => x.Id)
            };
        }
    }
}
