using ElectroPi.SupportTicket.Domain.Enums;
using ElectroPi.SupportTicket.Domain.Events;
using ElectroPi.SupportTicket.Domain.States;

namespace ElectroPi.SupportTicket.Domain.Entities
{
    public class  Ticket : AggregateRoot
    {
        private Ticket() { }

        private Ticket(Guid createdBy)
            : base(createdBy)
        {
        }
        public static Ticket Create(string title, string description, TicketPriority ticketPriority, Guid customerId, Guid createdBy)
        {
            Ticket ticket = new(createdBy)
            {
                Title = title,
                Description = description,
                Status = TicketState.Open,
                Priority = ticketPriority,
                CustomerId = customerId
            };

            ticket.AddDomainEvent(new TicketCreatedEvent(ticket.Id, title, description, ticketPriority, customerId, createdBy));

            return ticket;
        }

        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        public TicketState Status { get; private set; }

        public TicketPriority Priority { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? AssignedAgentId { get; set; }

        public IReadOnlyCollection<Comment> Comments => _comments;
        private readonly List<Comment> _comments = [];

        public IReadOnlyCollection<TimeEntry> TimeEntries => _timeEntries;
        private readonly List<TimeEntry> _timeEntries = [];

        public void AddComment(ITicketState state, string commentText)
        {
            state.EnsureCanAddComment();
            
            Comment comment = Comment.Create(commentText, Id, CreatedBy);

            _comments.Add(comment);

            AddDomainEvent(new CommentAddedEvent(comment.Id, comment.TicketId, comment.CreatedBy.Value, comment.Content));
        }

        public void AddTimeEntry(ITicketState state, TimeEntry timeEntry)
        {
            if (timeEntry.UserId != AssignedAgentId)
            {
                throw new InvalidOperationException("User can only add their own time entries.");
            }

            state.EnsureCanAddTimeEntry();
            _timeEntries.Add(timeEntry);

            AddDomainEvent(new TimeEntryAddedEvent(timeEntry.Id, timeEntry.TicketId, timeEntry.UserId, timeEntry.Duration, timeEntry.WorkDate));
        }

        public void AssignAgent(ITicketState state, Guid agentId, Guid assignedById)
        {
            state.EnsureCanAssign();
            AssignedAgentId = agentId;
            Status = TicketState.InProgress;

            AddDomainEvent(new AgentAssignedEvent(Id, agentId, assignedById));
        }

        public void ResolveTicket(ITicketState state, Guid ResolverAgentId)
        {
            if (AssignedAgentId != ResolverAgentId)
            {
                throw new InvalidOperationException("Only the assigned agent can resolve the ticket.");
            }

            state.EnsureCanResolve();
            Status = TicketState.Resolved;

            AddDomainEvent(new TicketResolvedEvent(Id, ResolverAgentId));
        }

        public void CloseTicket(ITicketState state, Guid CloserAgentId)
        {
            if (AssignedAgentId != CloserAgentId)
            {
                throw new InvalidOperationException("Only the assigned agent can close the ticket.");
            }

            state.EnsureCanClose();
            Status = TicketState.Closed;

            AddDomainEvent(new TicketClosedEvent(Id, CloserAgentId));
        }

        public void UpdatePriority(ITicketState state, TicketPriority newPriority, Guid prioritizedBy)
        {
            state.EnsureCanChangePriority();
            Priority = newPriority;

            AddDomainEvent(new TicketPriorityChangedEvent(Id, newPriority, prioritizedBy));
        }
    }
}
