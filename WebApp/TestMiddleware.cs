using WebApp.Models;

namespace WebApp
{
    public sealed class TestMiddleware
    {
        private RequestDelegate _next;

        public TestMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            if (string.Equals(context.Request.Path, "/test"))
            {
                await context.Response.WriteAsync($"There are {dataContext.Products.Count()} products\n");
                await context.Response.WriteAsync($"There are {dataContext.Categories.Count()} categories\n");
                await context.Response.WriteAsync($"There are {dataContext.Suppliers.Count()} suppliers\n");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
