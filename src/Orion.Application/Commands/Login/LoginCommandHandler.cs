using MediatR;
using OperationResult;
using Orion.Application.Services;
using Orion.Core.Entities.Users;
using Orion.Shared;

namespace Orion.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Email == request.Email);

            if (user is not null && BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                var response = new AuthResponseDto { Token = _tokenService.GenerateJwtToken(user) };

                return Result.Success(response);
            }

            return Result.Error<AuthResponseDto>(new UnauthorizedAccessException("Invalid Credentials"));
        }
    }
}
