namespace ElectroPi.SupportTicket.Application.Abstractions.Identity
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
        Guid? CustomerId { get; }
        bool IsAuthenticated { get; }
    }
}