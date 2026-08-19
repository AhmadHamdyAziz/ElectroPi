namespace ElectroPi.SupportTicket.Domain.Entities
{
    public class TimeEntry : EntityBase
    {
        public Guid TicketId { get; private set; }
        public Guid UserId { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTimeOffset WorkDate { get; private set; } = DateTimeOffset.UtcNow;
    }
}
