using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Authentication.Commands.Login
{
    public sealed class LoginHandler
        : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IAppDbContext _db;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginHandler(
            IAppDbContext db,
            IPasswordHasher<User> passwordHasher,
            IJwtTokenService jwtTokenService)
        {
            _db = db;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResult> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim();

            var user = await _db.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(
                    x => x.Email == email,
                    cancellationToken);

            if (user is null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            var token = _jwtTokenService.CreateToken(
                user.Id,
                user.CustomerId,
                user.Role.Name);

            return new LoginResult(
                new LoggedInUserDto(
                    user.Id,
                    user.Email,
                    user.Role.Name,
                    user.CustomerId
                ),
                token);
        }
    }
}