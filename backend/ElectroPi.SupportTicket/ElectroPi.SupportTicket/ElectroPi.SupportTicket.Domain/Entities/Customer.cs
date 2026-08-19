namespace ElectroPi.SupportTicket.Domain.Entities
{
    public class Customer : EntityBase
    {
        private Customer()
        {
        }

        private Customer(
            string name,
            Guid createdBy)
            : base(createdBy)
        {
            Name = name;
        }

        public string Name { get; private set; } = default!;

        public static Customer Create(
            string name,
            Guid createdBy)
        {
            return new Customer(name, createdBy);
        }
    }
}