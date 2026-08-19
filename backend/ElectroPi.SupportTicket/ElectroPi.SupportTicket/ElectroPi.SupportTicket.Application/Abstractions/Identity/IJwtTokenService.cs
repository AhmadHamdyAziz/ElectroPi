namespace ElectroPi.SupportTicket.Application.Abstractions.Identity
{
    public interface IJwtTokenService
    {
        string CreateToken(
            Guid userId,
            Guid? customerId,
            string roleName);
    }
}