using MediatR;
using OperationResult;
using Orion.Shared;

namespace Orion.Application.Commands.Login
{
    public class LoginCommand : IRequest<Result<AuthResponseDto>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
