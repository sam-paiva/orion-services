using MediatR;
using OperationResult;
using Orion.Shared;

namespace Orion.Application.Queries.GetProperties
{
    public class GetPropertiesQuery : IRequest<Result<IEnumerable<ImmobileDto>>>
    {
    }
}
