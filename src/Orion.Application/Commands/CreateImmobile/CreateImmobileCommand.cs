using MediatR;
using OperationResult;
using Orion.Core.Entities.Properties;
using Orion.Shared;
using Orion.Shared.Dtos;

namespace Orion.Application.Commands.CreateImmobile
{
    public class CreateImmobileCommand : IRequest<Result<ImmobileDto>>, ICommand, IUserRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Bedrooms { get; set; }
        public AddressDto? Address { get; set; }
        public string[] PhotosUrl { get; set; }
        public Decimal Price { get; set; }
        public AcquisitionType AcquisitionType { get; set; }
        public ImmobileType ImmobileType { get; set; }
        public string? WhatsappContact { get; set; }
        public Guid UserId { get; set; }
    }
}
