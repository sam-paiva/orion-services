using MediatR;
using OperationResult;
using Orion.Core.Entities.Properties;
using Orion.Shared;

namespace Orion.Application.Commands.CreateImmobile
{
    public class CreateImmobileCommand : IRequest<Result<ImmobileDto>>, ICommand
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string[] PhotosUrl { get; set; }
        public Decimal Price { get; set; }
        public AcquisitionType AcquisitionType { get; set; }
        public ImmobileType ImmobileType { get; set; }
    }
}
