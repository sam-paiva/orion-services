using MediatR;
using OperationResult;

namespace Orion.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result>, ICommand
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
