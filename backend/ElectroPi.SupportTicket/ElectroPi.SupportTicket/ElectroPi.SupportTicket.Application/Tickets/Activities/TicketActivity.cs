using ElectroPi.SupportTicket.Domain.Entities;

namespace ElectroPi.SupportTicket.Application.Tickets.Activities
{
    public class TicketActivity : EntityBase
    {
        public Guid TicketId { get; private set; }
        public Guid ActorId { get; private set; }
        public string ActivityType { get; private set; } = string.Empty;
        public DateTimeOffset OccurredAt { get; private set; }
        public string? Data { get; private set; }

        public static TicketActivity Create(
            Guid ticketId, 
            string activityType, 
            Guid actorId, 
            DateTimeOffset occurredAt, 
            string? data)
        {
            TicketActivity activity = new()
            {
                TicketId = ticketId,
                ActivityType = activityType,
                ActorId = actorId,
                OccurredAt = occurredAt,
                Data = data
            };
            return activity;
        }
    }
}
