using ElectroPi.SupportTicket.Application.Abstractions.DomainEvents;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Application.Tickets.Activities;
using ElectroPi.SupportTicket.Domain.Events;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ElectroPi.SupportTicket.Infrastructure.EventHandlers
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        public Task DispatchAsync(
            DbSet<TicketActivity> ticketActivities,
            IReadOnlyCollection<IDomainEvent> events,
            CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in events)
            {
                if (domainEvent is not AbstractTicketDomainEvent ticketEvent)
                    continue;

                var history = TicketActivity.Create(
                    ticketEvent.TicketId,
                    ticketEvent.GetType().Name,
                    ticketEvent.ActorId,
                    ticketEvent.OccurredAt,
                    SerializeData(ticketEvent));

                ticketActivities.Add(history);
            }

            return Task.CompletedTask;
        }

        private static string? SerializeData(AbstractTicketDomainEvent domainEvent)
        {
            return JsonSerializer.Serialize(
                domainEvent,
                domainEvent.GetType());
        }
    }
}
