using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Orion.API.OData;
using Orion.Application.Commands.CreateImmobile;
using Orion.Application.Queries.GetProperties;

namespace Orion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : AbstractController
    {
        public PropertiesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateImmobile(CreateImmobileCommand command, CancellationToken cancellationToken)
        {
            return await SendCommand(command, cancellationToken) switch
            {
                (true, var result) => CreatedAtAction(nameof(CreateImmobile),result),
                (_, _, var error) => ErrorResponse(error!)
            };
        }

        [HttpGet]
        [CustomQuery]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await SendCommand(new GetPropertiesQuery(), cancellationToken) switch
            {
                (true, var result) => Ok(result),
                (_, _, var error) => ErrorResponse(error!)
            };
        }
    }
}
