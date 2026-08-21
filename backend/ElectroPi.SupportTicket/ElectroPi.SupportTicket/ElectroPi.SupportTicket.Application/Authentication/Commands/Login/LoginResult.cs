namespace ElectroPi.SupportTicket.Application.Authentication.Commands.Login
{
    public sealed record LoginResult(
        LoggedInUserDto User,
        string AccessToken);
}