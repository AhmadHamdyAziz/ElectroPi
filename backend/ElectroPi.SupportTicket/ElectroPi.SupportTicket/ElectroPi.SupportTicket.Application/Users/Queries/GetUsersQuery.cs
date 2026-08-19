using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Users.DTOs;
using MediatR;

namespace ElectroPi.SupportTicket.Application.Users.Queries
{
    public sealed record GetUsersQuery(
            PaginationRequest Pagination,
            string? Email,
            string? RoleName)
        : IRequest<PaginationResponse<UserListItemDto>>;
}
