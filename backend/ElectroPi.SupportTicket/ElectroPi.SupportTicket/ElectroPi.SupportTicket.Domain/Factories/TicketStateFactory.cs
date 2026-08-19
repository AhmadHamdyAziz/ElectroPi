using ElectroPi.SupportTicket.Domain.Enums;
using ElectroPi.SupportTicket.Domain.States;

namespace ElectroPi.SupportTicket.Domain.Factories
{
    public sealed class TicketStateFactory : ITicketStateFactory
    {
        public ITicketState Create(TicketState ticketState)
        {
            return ticketState switch
            {
                TicketState.Open =>
                    new OpenTicketState(),

                TicketState.InProgress =>
                    new InProgressTicketState(),

                TicketState.Resolved =>
                    new ResolvedTicketState(),

                TicketState.Closed =>
                    new ClosedTicketState(),

                _ => throw new ArgumentOutOfRangeException(nameof(ticketState), ticketState, null)
            };
        }
    }
}
