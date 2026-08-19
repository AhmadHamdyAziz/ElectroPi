using MediatR;

namespace ElectroPi.SupportTicket.Application.Authentication.Commands.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password) : IRequest<LoginResult>;
}