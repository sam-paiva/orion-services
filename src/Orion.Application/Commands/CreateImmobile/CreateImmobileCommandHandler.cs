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
            Address address = new(request.Address!.City, request.Address.District, request.Address.State);
            Immobile immobile = new(request.Title!, request.Description!, request.Bedrooms, address, request.PhotosUrl, request.Price, request.AcquisitionType, request.ImmobileType, request.WhatsappContact!);
            user.AddImmobile(immobile);
            await _userRepository.UpdateAsync(user);

            var dto = _mapper.Map<ImmobileDto>(immobile);
            return Result.Success(dto);
        }
    }
}
