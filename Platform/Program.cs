using Microsoft.Extensions.Options;

namespace Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<MessageOptions>(options => options.CityName = "Ontario");
            var app = builder.Build();

            app.UseMiddleware<LocationMiddleWare>();

            //app.MapGet("/location", async (HttpContext context,
            //    IOptions<MessageOptions> msgOpts) =>
            //{
            //    MessageOptions options = msgOpts.Value;
            //    await context.Response.WriteAsync($"{options.CityName}, {options.CountryName}");
            //});

            #region middleware examples
            //app.Map("/branch", branch =>
            //    branch.Run(new QueryStringMiddleWare().Invoke));

            //// modify HTTPResponse after calling next()
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
            //});

            //// short-circuiting middleware
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/short")
            //    {
            //        await context.Response.WriteAsync("Request short circuited");
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});

            //// custom middleware
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Method == HttpMethods.Get
            //            && context.Request.Query["custom"] == "true")
            //    {
            //        context.Response.ContentType = "text/plain";
            //        await context.Response.WriteAsync("Lambda function-based MiddleWare\n");
            //    }
            //    await next();
            //});

            //// custom class-based middleware
            //app.UseMiddleware<QueryStringMiddleWare>();
            #endregion

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
