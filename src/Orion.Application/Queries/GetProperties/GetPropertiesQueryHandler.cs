using AutoMapper;
using MediatR;
using OperationResult;
using Orion.Core.Entities.Properties;
using Orion.Shared;
using System.Linq.Expressions;

namespace Orion.Application.Queries.GetProperties
{
    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, Result<IEnumerable<ImmobileDto>>>
    {
        private readonly IImmobileRepository _immobileRepository;
        private readonly IMapper _mapper;

        public GetPropertiesQueryHandler(IImmobileRepository immobileRepository, IMapper mapper)
        {
            _immobileRepository = immobileRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ImmobileDto>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Immobile, object>>[] IncludesList = new Expression<Func<Immobile, object>>[] { c => c.Address! };
            var immobiles = await _immobileRepository.GetAllAsync(includes: IncludesList);
            var result = _mapper.Map<IEnumerable<ImmobileDto>>(immobiles);

            return Result.Success(result);
        }
    }
}
