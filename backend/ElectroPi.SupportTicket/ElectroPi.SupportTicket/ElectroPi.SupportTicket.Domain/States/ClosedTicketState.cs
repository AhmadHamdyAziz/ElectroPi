namespace ElectroPi.SupportTicket.Domain.States
{
    public sealed class ClosedTicketState : ITicketState
    {
        public void EnsureCanAddActivity()
        {
            throw new InvalidOperationException("Ticket is closed");
        }

        public void EnsureCanAddComment()
        {
            throw new InvalidOperationException("Ticket is closed");
        }

        public void EnsureCanAddTimeEntry()
        {
            throw new InvalidOperationException("Ticket is closed");
        }

        public void EnsureCanAssign()
        {
            throw new InvalidOperationException("Ticket is closed");
        }

        public void EnsureCanChangePriority()
        {
            throw new InvalidOperationException("Ticket is closed");
        }

        public void EnsureCanClose()
        {
            throw new InvalidOperationException("Ticket is closed");
        }

        public void EnsureCanResolve()
        {
            throw new InvalidOperationException("Ticket is closed");
        }
    }
}
