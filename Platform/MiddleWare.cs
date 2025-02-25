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
}
