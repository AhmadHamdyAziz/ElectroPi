using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Users.Queries
{
    public sealed class GetRolesQueryHandler(
        IAppDbContext db)
        : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        public Task<List<RoleDto>> Handle(
            GetRolesQuery request,
            CancellationToken cancellationToken)
        {
            var roles = db.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync(cancellationToken);

            return roles;
        }
    }
}
