using ElectroPi.SupportTicket.Application.Authentication.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectroPi.SupportTicket.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login(
            LoginCommand command,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                command,
                cancellationToken);

            return Ok(result);
        }
    }
}