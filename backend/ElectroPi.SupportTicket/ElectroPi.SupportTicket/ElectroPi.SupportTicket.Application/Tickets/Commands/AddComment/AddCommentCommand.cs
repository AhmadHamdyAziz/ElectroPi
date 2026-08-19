using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroPi.SupportTicket.Application.Tickets.Commands.AddComment
{
    public sealed record AddCommentCommand(
        Guid TicketId,
        string Comment) : IRequest;
}
