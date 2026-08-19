using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElectroPi.SupportTicket.Infrastructure.Identity
{
    public sealed class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string CreateToken(
            Guid userId,
            Guid? customerId,
            string roleName)
        {
            var claims = new List<Claim>
            {
                new(
                    ClaimTypes.NameIdentifier,
                    userId.ToString()),
                new(
                    ClaimTypes.Role,
                    roleName)
            };

            if (customerId.HasValue)
            {
                claims.Add(
                    new Claim(
                        "customerId",
                        customerId.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _settings.ExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}