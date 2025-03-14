using Platform.Services;

using System.Runtime.Serialization;

namespace Platform
{
    public sealed class WeatherMiddleware
    {
        private readonly RequestDelegate _next;

        public WeatherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter0,
            IResponseFormatter formatter1, IResponseFormatter formatter2)
        {
            if (string.Equals(context.Request.Path, "/middleware/class"))
            {
                await formatter0.Format(context, string.Empty);
                await formatter1.Format(context, string.Empty);
                await formatter2.Format(context, string.Empty);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
