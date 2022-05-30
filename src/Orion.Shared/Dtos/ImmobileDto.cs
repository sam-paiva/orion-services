using Orion.Core.Entities.Properties;
using Orion.Shared.Dtos;

namespace Orion.Shared
{
    public class ImmobileDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Bedrooms { get; set; }
        public AddressDto? Address { get; set; }
        public string[]? PhotosUrl { get; set; }
        public Decimal Price { get; set; }
        public AcquisitionType AcquisitionType { get; set; }
        public ImmobileType ImmobileType { get; set; }
    }
}
