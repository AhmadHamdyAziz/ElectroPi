namespace ElectroPi.SupportTicket.Application.Tickets.DTOs
{
    public sealed record TicketCommentDto(
        Guid Id,
        string Comment,
        Guid CreatedBy,
        DateTimeOffset CreatedAt);
}
