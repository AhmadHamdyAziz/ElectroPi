using ElectroPi.SupportTicket.Application.Common;
using ElectroPi.SupportTicket.Application.Customers.Command;
using ElectroPi.SupportTicket.Application.Customers.DTOs;
using ElectroPi.SupportTicket.Application.Customers.Queries;
using ElectroPi.SupportTicket.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectroPi.SupportTicket.Api.Controllers
{
    [ApiController]
    [Route("api/customerManagement")]
    [Authorize]
    public sealed class CustomerManagementController(ISender sender)
        : ControllerBase
    {
        [HttpGet("filter")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<PaginationResponse<CustomerListItemDto>>> filter(
            [FromQuery]
            GetCustomersQuery query,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(query, cancellationToken);
            
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<Guid>> CreateCustomer(
            AddCustomerCommand command,
            CancellationToken cancellationToken)
        {
            await sender.Send(command, cancellationToken);
            return Ok();
        }
    }
}