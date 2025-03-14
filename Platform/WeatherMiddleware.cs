using Platform.Services;

namespace Platform
{
    public sealed class WeatherMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IResponseFormatter _formatter;

        public WeatherMiddleware(RequestDelegate next, IResponseFormatter formatter)
        {
            _next = next;
            //_formatter = formatter;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter)
        {
            if (string.Equals(context.Request.Path, "/middleware/class"))
                await formatter.Format(context, "Middleware Class: It is raining in London");
            else
                await _next(context);
        }
    }
}
