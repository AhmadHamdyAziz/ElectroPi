using ElectroPi.SupportTicket.Application.Tickets.Activities;
using ElectroPi.SupportTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Abstractions.Persistence
{
    public interface IAppDbContext
    {
        DbSet<Ticket> Tickets { get; }

        DbSet<TicketActivity> TicketActivities { get; }

        DbSet<Customer> Customers { get; }

        DbSet<User> Users { get; }

        DbSet<Role> Roles { get; }

        DbSet<Comment> Comments { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
