using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ElectroPi.SupportTicket.Infrastructure.Identity
{
    public sealed class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated
            == true;

        public Guid? UserId =>
            GetGuidClaim(ClaimTypes.NameIdentifier);

        public Guid? CustomerId =>
            GetGuidClaim("customerId");

        private Guid? GetGuidClaim(string claimType)
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirst(claimType)?.Value;

            return Guid.TryParse(value, out var id)
                ? id
                : null;
        }
    }
}