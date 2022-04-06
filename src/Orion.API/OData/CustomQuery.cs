using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Orion.API.Infra;

namespace Orion.API.OData
{
    public class CustomQueryAttribute : EnableQueryAttribute
    {
        public CustomQueryAttribute()
        {
            PageSize = 100;
            HandleNullPropagation = HandleNullPropagationOption.False;
        }

        public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions)
        {
            base.ValidateQuery(request, queryOptions);
            request.HttpContext.Items.Add(nameof(ODataQueryOptions), queryOptions);
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is not null) return;

            base.OnActionExecuted(actionExecutedContext);

            var oData = actionExecutedContext.HttpContext.ODataFeature();
            if(oData.TotalCount.HasValue && actionExecutedContext.Result is ObjectResult obj && obj.Value is IQueryable queryable)
                actionExecutedContext.Result = new ObjectResult(new PaginatedResult(oData.TotalCount, queryable)) { StatusCode = 200};
        }
    }
}
