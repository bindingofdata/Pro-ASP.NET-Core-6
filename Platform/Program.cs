namespace Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Map("/branch", branch =>
                branch.Run(new QueryStringMiddleWare().Invoke));

            // modify HTTPResponse after calling next()
            app.Use(async (context, next) =>
            {
                await next();
                await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
            });

            // short-circuiting middleware
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/short")
                {
                    await context.Response.WriteAsync("Request short circuited");
                }
                else
                {
                    await next();
                }
            });

            // custom middleware
            app.Use(async (context, next) =>
            {
                if (context.Request.Method == HttpMethods.Get
                        && context.Request.Query["custom"] == "true")
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Lambda function-based MiddleWare\n");
                }
                await next();
            });

            // custom class-based middleware
            app.UseMiddleware<QueryStringMiddleWare>();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
