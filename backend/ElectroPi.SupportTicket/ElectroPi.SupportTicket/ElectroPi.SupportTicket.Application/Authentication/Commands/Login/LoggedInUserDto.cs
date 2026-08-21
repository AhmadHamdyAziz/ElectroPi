namespace ElectroPi.SupportTicket.Application.Authentication.Commands.Login
{
    public sealed record LoggedInUserDto(
        Guid Id,
        string Email,
        string RoleName,
        Guid? CustomerId);
}