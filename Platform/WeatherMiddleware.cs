namespace Platform
{
    public sealed class WeatherMiddleware
    {
        private readonly RequestDelegate _next;

        public WeatherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (string.Equals(context.Request.Path, "/middleware/class"))
                await context.Response.WriteAsync("Middleware Class: It is raining in London");
            else
                await _next(context);
        }
    }
}
