using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Customers.DTOs;
using MediatR;

namespace ElectroPi.SupportTicket.Application.Customers.Queries
{
    public sealed record GetCustomersQuery(
        PaginationRequest Pagination,
        string? Name
        )
        : IRequest<PaginationResponse<CustomerListItemDto>>;
}
