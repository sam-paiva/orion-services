using FluentValidation;
using Orion.Application.Commands.CreateImmobile;
using Orion.Core.Entities.Users;

namespace Orion.Application.Validators
{
    public class CreateImmobileCommandValidator : AbstractValidator<CreateImmobileCommand>
    {
        public CreateImmobileCommandValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.UserId).MustAsync(async (id, cancellation) =>
            {
                var user = await userRepository.GetAsync(x => x.Id == id);
                return user is not null;
            }).WithMessage("{PropertyName} User not found");

            RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} can not be empty or null");
            RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} can not be empty or null");
            RuleFor(x => x.Price).NotEmpty().WithMessage("{PropertyName} invalid, empty or null");
            RuleFor(x => x.Address).NotEmpty().WithMessage("{PropertyName} can not be empty or null");
        }
    }
}
