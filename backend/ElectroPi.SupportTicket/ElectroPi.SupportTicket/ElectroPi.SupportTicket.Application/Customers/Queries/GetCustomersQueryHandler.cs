using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Customers.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Customers.Queries
{
    public sealed class GetCustomersQueryHandler(
        IAppDbContext db)
        : IRequestHandler<GetCustomersQuery, PaginationResponse<CustomerListItemDto>>
    {
        public async Task<PaginationResponse<CustomerListItemDto>> Handle(
            GetCustomersQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Customer> query = db.Customers
                .AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(c => c.Name.Contains(request.Name));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip(request.Pagination.Skip)
                .Take(request.Pagination.PageSize)
                .Select(x => new CustomerListItemDto(
                    x.Id,
                    x.Name,
                    x.CreatedAt))
                .ToListAsync(cancellationToken);
            return new PaginationResponse<CustomerListItemDto>(items, request.Pagination.PageNumber, request.Pagination.PageSize, totalCount);
        }
    }
}
