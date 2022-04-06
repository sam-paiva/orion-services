using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Orion.Shared;

namespace Orion.API.OData
{
    public static class ODataHelper
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<ImmobileDto>("Properties");
            return builder.GetEdmModel();
        }
    }
}
