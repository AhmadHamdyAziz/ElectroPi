using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using ElectroPi.SupportTicket.Domain.Entities;


namespace ElectroPi.SupportTicket.Application.Tickets.Extensions
{
    internal static class Mapper
    {
        public static TicketItemDto Map(this Ticket? ticket, PaginationResponse<TicketCommentDto> comments)
        {
            if (ticket is not null)
            {
                return new TicketItemDto(ticket.Id, ticket.Title, ticket.Description, ticket.Status, ticket.Priority, ticket.CustomerId, "", ticket.AssignedAgentId, "", ticket.CreatedAt, comments);
            }
            return null;
        }
    }
}
