namespace ElectroPi.SupportTicket.Domain.Entities
{
    public class Comment : EntityBase
    {
        private Comment()
        {
        }

        private Comment(Guid? CreatedBy)
            : base(CreatedBy)
        {
        }

        public string Content { get; private set; } = default!;
        public Guid TicketId { get; private set; }

        public static Comment Create(string commentText, Guid ticketId, Guid? createdBy)
        {
            Comment comment = new(createdBy)
            {
                Content = commentText,
                TicketId = ticketId
            };
            return comment;
        }
    }
}
