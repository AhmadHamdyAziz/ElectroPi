namespace ElectroPi.SupportTicket.Application.Tickets.DTOs
{
    public sealed record TicketActivityDto(
        Guid Id,
        Guid TicketId,
        string ActivityType,
        Guid? UserId,
        DateTimeOffset OccurredAt,
        string? Data);
}
