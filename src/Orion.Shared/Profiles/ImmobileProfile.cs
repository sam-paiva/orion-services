using AutoMapper;
using Orion.Core.Entities.Properties;

namespace Orion.Shared
{
    public sealed class ImmobileProfile : Profile
    {
        public ImmobileProfile()
        {
            CreateMap<Immobile, ImmobileDto>().ReverseMap();
        }
    }
}
