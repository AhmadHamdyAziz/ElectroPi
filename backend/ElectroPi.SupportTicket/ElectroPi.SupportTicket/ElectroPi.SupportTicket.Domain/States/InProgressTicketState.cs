namespace ElectroPi.SupportTicket.Domain.States
{
    public sealed class InProgressTicketState : ITicketState
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
            throw new InvalidOperationException("Ticket already assigned to an agent");
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
        }
    }
}
