namespace ElectroPi.SupportTicket.Domain.Entities
{
    public class User : EntityBase
    {
        private User()
        {
        }

        private User(
            string email,
            Guid roleId,
            Guid? createdBy = null,
            Guid? customerId = null)
            : base(createdBy)
        {
            CustomerId = customerId;
            Email = email;
            RoleId = roleId;
        }

        public Guid? CustomerId { get; private set; }

        public string Email { get; private set; } = default!;

        public string PasswordHash { get; private set; } = default!;

        public Guid? RoleId { get; private set; }

        public Role? Role { get; private set; } = null!;

        public static User Create(
            string email,
            Guid roleId,
            Guid? createdBy = null,
            Guid? customerId = null)
        {
            return new User(
                email,
                roleId,
                createdBy,
                customerId);
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}