using ElectroPi.SupportTicket.Application.Abstractions.DomainEvents;
using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Domain.Entities;
using ElectroPi.SupportTicket.Domain.Factories;
using ElectroPi.SupportTicket.Infrastructure.EventHandlers;
using ElectroPi.SupportTicket.Infrastructure.Identity;
using ElectroPi.SupportTicket.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ElectroPi.SupportTicket.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "DefaultConnection connection string was not found.");

            services.AddHttpContextAccessor();

            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddScoped<ITicketStateFactory,  TicketStateFactory>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddDbContext<AppDbContext>(
                options =>
                options.UseSqlServer(connectionString)
                );

            services.AddScoped<IAppDbContext>(
                provider => provider.GetRequiredService<AppDbContext>());


            var jwtSettings =
                configuration
                    .GetSection("Jwt")
                    .Get<JwtSettings>()
                ?? throw new InvalidOperationException(
                    "JWT configuration is missing.");

            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = jwtSettings.Issuer,

                            ValidateAudience = true,
                            ValidAudience = jwtSettings.Audience,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.Key)),

                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.Zero,

                            RoleClaimType = ClaimTypes.Role
                        };
                });
        }
    }
}
