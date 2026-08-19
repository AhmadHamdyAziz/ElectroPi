using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Users.Commands.CreateUser;
using ElectroPi.SupportTicket.Application.Users.DTOs;
using ElectroPi.SupportTicket.Application.Users.Queries;
using ElectroPi.SupportTicket.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectroPi.SupportTicket.Api.Controllers
{
    [ApiController]
    [Route("api/userManagement")]
    [Authorize(Roles = RoleNames.Admin)]
    public sealed class UserManagementController(ISender sender) : ControllerBase
    {
        [HttpGet("users")]
        public async Task<ActionResult<PaginationResponse<UserListItemDto>>> GetUsers(
            [FromQuery]
            GetUsersQuery query,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDto>>> GetRoles(
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetRolesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost("users")]
        public async Task<ActionResult<Guid>> CreateUser(
            CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            await sender.Send(command, cancellationToken);
            return Ok();
        }
    }
}