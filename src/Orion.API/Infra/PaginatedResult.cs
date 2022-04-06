using System.Collections;

namespace Orion.API.Infra
{
    public class PaginatedResult
    {
        public PaginatedResult(long? counts, IEnumerable items)
        {
            Counts = counts;
            Items = items;
        }

        public long? Counts { get; }
        public IEnumerable Items { get; }
    }
}
