namespace ElectroPi.SupportTicket.Domain.Events
{
    public class CommentAddedEvent(
        Guid commentId,
        Guid ticketId,
        Guid UserId,
        string content)
        : AbstractTicketDomainEvent(ticketId, UserId)
    {
        public Guid CommentId { get; private set; } = commentId;
        public string Content { get; private set; } = content;
    }
}
