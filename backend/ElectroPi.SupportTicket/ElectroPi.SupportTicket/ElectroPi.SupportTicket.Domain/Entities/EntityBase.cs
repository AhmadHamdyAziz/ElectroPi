namespace ElectroPi.SupportTicket.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() { }
        protected EntityBase(Guid? createdby)
        {
            CreatedBy=createdby;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid? CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
        public Guid? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}
