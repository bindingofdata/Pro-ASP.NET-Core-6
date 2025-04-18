using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    public sealed class SimpleCacheAttribute : Attribute, IResourceFilter
    {
        private readonly Dictionary<PathString, IActionResult> _cachedResponses = new();

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            PathString path = context.HttpContext.Request.Path;
            if (_cachedResponses.TryGetValue(path, out IActionResult? value))
            {
                context.Result = value;
                _cachedResponses.Remove(path);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (context.Result != null)
            {
                _cachedResponses.Add(context.HttpContext.Request.Path, context.Result);
            }
        }
    }
}
