using Microsoft.Extensions.Options;

namespace Platform
{
    public sealed class QueryStringMiddleWare
    {
        private readonly RequestDelegate? _next;

        public QueryStringMiddleWare() { }

        public QueryStringMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get
                    && context.Request.Query["custom"] == "true")
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }
                await context.Response.WriteAsync("Class-based MiddleWare\n");
            }

            if (_next != null)
            {
                await _next(context);
            }
        }
    }

    public sealed class LocationMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly MessageOptions _options;

        public LocationMiddleWare(RequestDelegate requestDelegate, IOptions<MessageOptions> messageOptions)
        {
            _next = requestDelegate;
            _options = messageOptions.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{_options.CityName}, {_options.CountryName}");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
