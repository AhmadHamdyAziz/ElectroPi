using ElectroPi.SupportTicket.Domain.Enums;
using ElectroPi.SupportTicket.Domain.States;

namespace ElectroPi.SupportTicket.Domain.Factories
{
    public interface ITicketStateFactory
    {
        ITicketState Create(TicketState ticketState);
    }
}
