using Platform.Services;

namespace Platform
{
    public sealed class WeatherMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IResponseFormatter _formatter;

        public WeatherMiddleware(RequestDelegate next, IResponseFormatter formatter)
        {
            _next = next;
            _formatter = formatter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (string.Equals(context.Request.Path, "/middleware/class"))
                await _formatter.Format(context, "Middleware Class: It is raining in London");
            else
                await _next(context);
        }
    }
}
