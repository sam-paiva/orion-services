using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orion.API.Infra;
using Orion.Application;
using Orion.Application.Exceptions;
using System.Collections;

namespace Orion.API.Controllers
{
    public abstract class AbstractController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AbstractController(IMediator mediator) => _mediator = mediator;

        protected Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            FillUserId(request);
            return _mediator.Send(request, cancellationToken);
        }

        protected IActionResult ErrorResponse(Exception exception)
        {
            return exception switch
            {
                BaseRequestException e => ReturnResultErrors(e.Errors),
                Exception e => BadRequest(new { e.Message })
            };
        }

        private IActionResult ReturnResultErrors(IReadOnlyDictionary<string, object> keyValues)
        {
            var errorsList = new List<string>();

            foreach (var item in keyValues.Values)
            {
                string[] arr = ((IEnumerable)item).Cast<object>()
                                 .Select(x => x.ToString())
                                 .ToArray()!;

                foreach (var message in arr)
                    errorsList.Add(message);
            }

            return UnprocessableEntity(errorsList);
        }

        private void FillUserId<TRequest>(TRequest request)
        {
            if (request is not IUserRequest authenticable)
                return;

            authenticable.UserId = Guid.Parse(User.GetUserId());
        }
    }
}
