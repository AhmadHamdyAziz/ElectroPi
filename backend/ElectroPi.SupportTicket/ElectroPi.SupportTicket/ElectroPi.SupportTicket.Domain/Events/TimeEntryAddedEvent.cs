namespace ElectroPi.SupportTicket.Domain.Events
{
    public class TimeEntryAddedEvent(
        Guid timeEntryId,
        Guid ticketId,
        Guid UserId,
        TimeSpan duration,
        DateTimeOffset workDate)
        : AbstractTicketDomainEvent(ticketId, UserId)
    {
        public Guid TimeEntryId { get; private set; } = timeEntryId;
        public TimeSpan Duration { get; private set; } = duration;
        public DateTimeOffset WorkDate { get; private set; } = workDate;
    }
}
