
namespace ElectroPi.SupportTicket.Application.Customers.DTOs
{
    public sealed record CustomerListItemDto(Guid Id, string Name, DateTimeOffset CreatedAt);
}
