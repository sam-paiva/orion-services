using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Orion.Application.Exceptions;

namespace Orion.Application.Pipelines
{
    public class FailFastRequestBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public FailFastRequestBehaviour(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var errors = await ValidateAsync(request, cancellationToken);

            if (!errors.Any())
                return await next.Invoke();

            var requestException = new ValidationRequestException(errors);

            if (ResultCaster.UsesOperationResult<TResponse>())
                return ResultCaster.ErrorResult<TResponse>(requestException);

            throw requestException;
        }

        private async Task<List<ValidationFailure>> ValidateAsync(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var result = await _validator.ValidateAsync(context, cancellationToken);

            if (result.IsValid)
                return new List<ValidationFailure>();

            return result.Errors;
        }
    }
}
