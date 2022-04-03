using AutoMapper;
using MediatR;
using OperationResult;
using Orion.Core.Entities.Properties;
using Orion.Core.Entities.Users;
using Orion.Shared;

namespace Orion.Application.Commands.CreateImmobile
{
    public class CreateImmobileCommandHandler : IRequestHandler<CreateImmobileCommand, Result<ImmobileDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateImmobileCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<ImmobileDto>> Handle(CreateImmobileCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetAsync(x => x.Id == request.UserId);
            Immobile immobile = new(request.Title!, request.Description!, request.Address!, request.PhotosUrl, request.Price, request.AcquisitionType, request.ImmobileType);

            user.AddImmobile(immobile);

            await _userRepository.UpdateAsync(user);

            var dto = _mapper.Map<ImmobileDto>(immobile);

            return Result.Success(dto);
        }
    }
}
