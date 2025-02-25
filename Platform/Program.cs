namespace Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // custom middleware
            app.Use(async (context, next) =>
            {
                if (context.Request.Method == HttpMethods.Get
                        && context.Request.Query["custom"] == "true")
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Lambda function-based MiddleWare \n");
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
