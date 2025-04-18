using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    public sealed class SimpleAsyncCacheAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly Dictionary<PathString, IActionResult> _cachedResponses = new();

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            PathString path = context.HttpContext.Request.Path;
            if (_cachedResponses.TryGetValue(path, out IActionResult? value))
            {
                context.Result = value;
                _cachedResponses.Remove(path);
            }
            else
            {
                ResourceExecutedContext execContext = await next();
                if (execContext.Result != null)
                {
                    _cachedResponses.Add(path, execContext.Result);
                }
            }
        }
    }
}
