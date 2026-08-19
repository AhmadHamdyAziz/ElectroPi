using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Tickets.Commands.AddComment;
using ElectroPi.SupportTicket.Application.Tickets.Commands.AssignAgent;
using ElectroPi.SupportTicket.Application.Tickets.Commands.CreateTicket;
using ElectroPi.SupportTicket.Application.Tickets.DTOs;
using ElectroPi.SupportTicket.Application.Tickets.Queries;
using ElectroPi.SupportTicket.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectroPi.SupportTicket.Api.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public sealed class TicketsController(ISender sender) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = RoleNames.Customer)]
        public async Task<ActionResult<Guid>> Create(
            CreateTicketCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                command,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleNames.Customer},{RoleNames.Admin},{RoleNames.Agent}")]
        public async Task<ActionResult<PaginationResponse<TicketListItemDto>>> GetList(
            [FromQuery]
            GetTicketsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{ticketId}")]
        [Authorize(Roles = $"{RoleNames.Customer},{RoleNames.Admin},{RoleNames.Agent}")]
        public async Task<ActionResult<TicketItemDto>> GetTicket(
             Guid ticketId,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetTicketDetailsQuery(ticketId), cancellationToken);

            return Ok(result);
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("{ticketId:guid}/assign")]
        public async Task<IActionResult> Assign(
            Guid ticketId,
            AssignAgentCommand request,
            CancellationToken cancellationToken)
        {
            await sender.Send(
                new AssignAgentCommand(
                    ticketId,
                    request.AgentId),
                cancellationToken);

            return NoContent();
        }

        [Authorize]
        [HttpGet("{ticketId:guid}/comments")]
        public async Task<ActionResult<PaginationResponse<TicketCommentDto>>> GetComments(
            Guid ticketId,
            [FromQuery]
            PaginationRequest pagination,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetTicketCommentsQuery(ticketId, pagination),
                cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("{ticketId:guid}/comments")]
        public async Task<ActionResult> AddComment(
            Guid ticketId,
            AddCommentCommand command,
            CancellationToken cancellationToken)
        {
            await sender.Send(new AddCommentCommand(ticketId, command.Comment), cancellationToken);

            return NoContent();
        }
    }
}