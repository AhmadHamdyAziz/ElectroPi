namespace ElectroPi.SupportTicket.Domain.States
{
    public sealed class ResolvedTicketState : ITicketState
    {
        public void EnsureCanAddActivity()
        {
        }

        public void EnsureCanAddComment()
        {
        }

        public void EnsureCanAddTimeEntry()
        {
        }

        public void EnsureCanAssign()
        {
            throw new InvalidOperationException("Ticket is resolved");
        }

        public void EnsureCanChangePriority()
        {
            throw new InvalidOperationException("Ticket is resolved");
        }

        public void EnsureCanClose()
        {
        }

        public void EnsureCanResolve()
        {
            throw new InvalidOperationException("Ticket is resolved");
        }
    }
}
