using AutoMapper;
using MediatR;
using OperationResult;
using Orion.Core.Entities.Properties;
using Orion.Shared;

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
            var immobiles = await _immobileRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ImmobileDto>>(immobiles);

            return Result.Success(result);
        }
    }
}
