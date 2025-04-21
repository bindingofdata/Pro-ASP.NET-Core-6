using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApp.Filters
{
    public sealed class ResultDiagnosticsAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.HttpContext.Request.Query.ContainsKey("diag"))
            {
                Dictionary<string, string?> diagData = new()
                {
                    {"Result type", context.Result.GetType().Name}
                };

                if (context.Result is ViewResult viewResult)
                {
                    diagData["View Name"] = viewResult.ViewName;
                    diagData["Model Type"] = viewResult.ViewData?.Model?.GetType().Name;
                    diagData["Model Data"] = viewResult.ViewData?.Model?.ToString();
                }
                else if (context.Result is PageResult pageResult)
                {
                    diagData["Model Type"] = pageResult.Model.GetType().Name;
                    diagData["Model Data"] = pageResult.ViewData?.Model?.ToString();
                }
                context.Result = new ViewResult()
                {
                    ViewName = "/Views/Shared/Message.cshtml",
                    ViewData = new ViewDataDictionary(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    {
                        Model = diagData
                    }
                };
            }
            await next();
        }
    }
}
