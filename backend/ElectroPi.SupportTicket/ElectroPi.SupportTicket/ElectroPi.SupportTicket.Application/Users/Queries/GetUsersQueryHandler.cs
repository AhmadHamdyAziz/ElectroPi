using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Users.DTOs;
using ElectroPi.SupportTicket.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Users.Queries
{
    public class GetUsersQueryHandler(
        IAppDbContext db)
        : IRequestHandler<GetUsersQuery, PaginationResponse<UserListItemDto>>
    {
        public async Task<PaginationResponse<UserListItemDto>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            Guid? roleId = null;

            if (string.IsNullOrWhiteSpace(request.RoleName))
            {
                var role = await db.Roles.AsNoTracking().SingleOrDefaultAsync(r => r.Name == request.RoleName, cancellationToken);
                if (role != null)
                {
                    roleId = role.Id;
                }
            }

            IQueryable<User> query = db.Users
                .AsNoTracking();

            if (roleId.HasValue)
            {
                query = query.Where(u => u.RoleId == roleId.Value);
            }

            if(!string.IsNullOrWhiteSpace(request.Email))
            {
                query = query.Where(u => u.Email.Contains(request.Email));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip(request.Pagination.Skip)
                .Take(request.Pagination.PageSize)
                .Select(x => new UserListItemDto(
                    x.Id,
                    x.Email,
                    x.RoleId,
                    x.Role != null ? x.Role.Name : "",
                    x.CustomerId,
                    x.CreatedAt))
                .ToListAsync(cancellationToken);

            return new PaginationResponse<UserListItemDto>(
                items,
                request.Pagination.PageNumber,
                request.Pagination.PageSize,
                totalCount);
        }
    }
}
