using MediatR;
using OperationResult;
using Orion.Core.Entities.Users;

namespace Orion.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new(request.FirstName!, request.LastName!, request.Email!, BCrypt.Net.BCrypt.HashPassword(request.Password));
            await _userRepository.AddAsync(user);

            return Result.Success();
        }
    }
}
