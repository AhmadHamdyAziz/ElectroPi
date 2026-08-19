namespace ElectroPi.SupportTicket.Domain.States
{
    public sealed class OpenTicketState : ITicketState
    {
        public void EnsureCanAddActivity()
        {
        }

        public void EnsureCanAddComment()
        {
        }

        public void EnsureCanAddTimeEntry()
        {
            throw new InvalidOperationException("Ticket Should be assigned first");
        }

        public void EnsureCanAssign()
        {
        }

        public void EnsureCanChangePriority()
        {
        }

        public void EnsureCanClose()
        {
            throw new InvalidOperationException("Ticket Should be resolved first");
        }

        public void EnsureCanResolve()
        {
            throw new InvalidOperationException("Ticket Should be in progress first");
        }
    }
}
