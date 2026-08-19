namespace ElectroPi.SupportTicket.Application.Users.DTOs
{
    public sealed class UserListItemDto(Guid id, string email, Guid? roleId, string roleName, Guid? customerId, DateTimeOffset createdAt)
    {
        public Guid Id { get; set; } = id;
        public string Email { get; set; } = email;
        public string RoleName { get; set; } = roleName;
        public Guid? RoleId { get; set; } = roleId;
        public Guid? CustomerId { get; set; } = customerId;
        public DateTimeOffset CreatedAt { get; set; } = createdAt;
    }
}
