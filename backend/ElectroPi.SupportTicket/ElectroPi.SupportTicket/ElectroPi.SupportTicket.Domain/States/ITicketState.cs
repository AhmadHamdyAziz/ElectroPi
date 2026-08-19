using ElectroPi.SupportTicket.Domain.Entities;
using ElectroPi.SupportTicket.Domain.Enums;

namespace ElectroPi.SupportTicket.Domain.States
{
    public interface ITicketState
    {
        void EnsureCanAddComment();
        void EnsureCanAddTimeEntry();
        void EnsureCanAddActivity();
        void EnsureCanChangePriority();

        void EnsureCanAssign();
        void EnsureCanResolve();
        void EnsureCanClose();
    }
}
