using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orion.Application.Commands.CreateUser;
using Orion.Application.Commands.Login;

namespace Orion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AbstractController
    {
        public UsersController(IMediator mediator) : base(mediator) { }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return await SendCommand(command, cancellationToken) switch
            {
                (true, _) => Ok(),
                (_, var error) => ErrorResponse(error!)
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            return await SendCommand(command, cancellationToken) switch
            {
                (true, var result) => Ok(result),
                (_, _, var error) => error is UnauthorizedAccessException ? Unauthorized() : ErrorResponse(error!)
            };
        }
    }
}
