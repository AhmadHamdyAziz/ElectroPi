namespace ElectroPi.SupportTicket.Application.Tickets.Queries
{
    public sealed record TicketSortOption(
        TicketSortField SortField = TicketSortField.CreatedAt,
        SortDirection SortDirection = SortDirection.Descending);
}
