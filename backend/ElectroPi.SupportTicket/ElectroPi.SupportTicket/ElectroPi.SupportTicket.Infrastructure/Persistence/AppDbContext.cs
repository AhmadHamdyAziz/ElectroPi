using ElectroPi.SupportTicket.Application.Abstractions.DomainEvents;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Tickets.Activities;
using ElectroPi.SupportTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectroPi.SupportTicket.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher domainEventDispatcher) : DbContext(options), IAppDbContext
    {
        public DbSet<Ticket> Tickets => Set<Ticket>();

        public DbSet<TicketActivity> TicketActivities => Set<TicketActivity>();

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<Comment> Comments => Set<Comment>();

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker
                .Entries<AggregateRoot>()
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            if (domainEvents.Count > 0)
            {
                await domainEventDispatcher.DispatchAsync(
                    TicketActivities,
                    domainEvents,
                    cancellationToken);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
            {
                entry.Entity.ClearDomainEvents();
            }

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    }
}
