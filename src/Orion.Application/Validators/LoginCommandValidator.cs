using FluentValidation;
using Orion.Application.Commands.Login;
using Orion.Core.Entities.Users;

namespace Orion.Application.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
            {
                var user = await userRepository.GetAsync(x => x.Email == email);
                return user is not null;
            }).WithMessage("Email not found");

            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("{PropertyName} invalid, empty or null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} can not be empty or null");
        }
    }
}
