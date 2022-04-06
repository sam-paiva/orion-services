using AutoMapper;
using Orion.Core.Entities.Properties;
using Orion.Shared.Dtos;

namespace Orion.Shared
{
    public sealed class ImmobileProfile : Profile
    {
        public ImmobileProfile()
        {
            CreateMap<Immobile, ImmobileDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
