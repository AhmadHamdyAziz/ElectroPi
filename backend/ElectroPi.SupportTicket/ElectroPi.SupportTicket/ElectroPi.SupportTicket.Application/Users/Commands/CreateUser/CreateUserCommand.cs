using MediatR;

namespace ElectroPi.SupportTicket.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
            string Email,
            string Password,
            Guid RoleId,
            Guid? CustomerId) : IRequest;
}
