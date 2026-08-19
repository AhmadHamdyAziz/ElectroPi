namespace ElectroPi.SupportTicket.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = null!;

        private Role()
        {
        }

        public static Role Create(string name)
        {
            return new Role(name);
        }

        private Role(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Role name is required.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
        }
    }
}