using ElectroPi.SupportTicket.Application.Tickets.Activities;
using ElectroPi.SupportTicket.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace ElectroPi.SupportTicket.Application.Abstractions.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(
            DbSet<TicketActivity> ticketActivities,
            IReadOnlyCollection<IDomainEvent> domainEvents,
            CancellationToken cancellationToken = default);
    }
}
