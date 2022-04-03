using Orion.Core.Entities.Properties;

namespace Orion.Infra.Data.Repositories
{
    public class ImmobileRepository : BaseRepository<Immobile>, IImmobileRepository
    {
        public ImmobileRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
