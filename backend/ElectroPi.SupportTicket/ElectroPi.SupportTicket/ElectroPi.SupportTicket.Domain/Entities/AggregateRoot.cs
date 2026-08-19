using ElectroPi.SupportTicket.Domain.Events;

namespace ElectroPi.SupportTicket.Domain.Entities
{
    public abstract class AggregateRoot : EntityBase
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid createdby)
            : base(createdby)
        {
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents =>
            _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
