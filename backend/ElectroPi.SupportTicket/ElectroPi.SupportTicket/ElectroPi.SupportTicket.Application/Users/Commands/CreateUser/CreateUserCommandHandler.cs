using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Domain.Constants;
using ElectroPi.SupportTicket.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler(
        IAppDbContext db,
        IPasswordHasher<User> passwordHasher,
        ICurrentUser currentUser) : IRequestHandler<CreateUserCommand>
    {
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var role = await db.Roles.FindAsync([request.RoleId], cancellationToken: cancellationToken);

            if (role is null)
            {
                throw new InvalidOperationException($"Role with ID {request.RoleId} does not exist.");
            }

            var isCustomerRole =
                role.Name == RoleNames.Customer;

            if (isCustomerRole && request.CustomerId is null)
            {
                throw new ValidationException(
                    "CustomerId is required for Customer users.");
            }

            if(isCustomerRole && request.CustomerId is not null)
            {
                var customerExists = await db.Customers.AnyAsync(c => c.Id == request.CustomerId, cancellationToken);
                if (!customerExists)
                {
                    throw new ValidationException(
                        $"Customer with ID {request.CustomerId} does not exist.");
                }
            }

            if (!isCustomerRole && request.CustomerId is not null)
            {
                throw new ValidationException(
                    "CustomerId must not be specified for this role.");
            }

            if (await db.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                throw new ValidationException(
                    $"A user with the email '{request.Email}' already exists.");
            }

            var user = User.Create(
                request.Email,
                request.RoleId,
                currentUser.UserId,
                request.CustomerId);
            var passwordHash = passwordHasher.HashPassword(user, request.Password);
            user.SetPasswordHash(passwordHash);

            await db.Users.AddAsync(user, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
