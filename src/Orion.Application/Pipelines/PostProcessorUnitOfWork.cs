using MediatR;
using MediatR.Pipeline;
using Orion.Application.Commands;
using Orion.Core;

namespace Orion.Application.Pipelines
{
    public class PostProcessorUnitOfWork<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest: IRequest<TResponse>, ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public PostProcessorUnitOfWork(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task Process(TRequest request, TResponse Result, CancellationToken cancellationToken)
        {
            bool saveChanges = true;
            if (ResultCaster.UsesOperationResult<TResponse>())
            {
                dynamic result = Result!;
                saveChanges = result.IsSuccess;
            }

            if (saveChanges)
            {
                await _unitOfWork.Commit();
                //Save Event Log
            }
        }
    }
}
