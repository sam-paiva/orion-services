using FluentValidation;
using Orion.Application.Commands.CreateUser;
using Orion.Core.Entities.Users;

namespace Orion.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
            {
               var user = await userRepository.GetAsync(x => x.Email == email);
                return user is null;
            }).WithMessage("{PropertyName} already used");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} can not be empty or null");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} can not be empty or null");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("{PropertyName} invalid, empty or null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} can not be empty or null");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("{PropertyName} must have minimun 8 characters");
        }
    }
}
