using ElectroPi.SupportTicket.Application.Users.DTOs;
using MediatR;

namespace ElectroPi.SupportTicket.Application.Users.Queries
{
    public sealed record GetRolesQuery
        : IRequest<List<RoleDto>>;
}
