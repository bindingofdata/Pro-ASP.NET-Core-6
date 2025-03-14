using Platform.Services;

namespace Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Singleton service example
            //builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

            // Transient service example
            //builder.Services.AddTransient<IResponseFormatter, GuidService>();

            // Scoped service example
            builder.Services.AddScoped<IResponseFormatter, GuidService>();

            var app = builder.Build();

            app.UseMiddleware<WeatherMiddleware>();

            // Dependency Injection middleware example
            app.MapGet("middleware/function", async (HttpContext context, IResponseFormatter formatter) =>
                await formatter.Format(context, "Middleware Function: It is snowing in Chicago"));

            // Dependency Injection in endpoint class example
            //app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);
            app.MapEndpoint<WeatherEndpoint>("endpoint/class");

            // Dependency Injection endpoint example
            app.MapGet("endpoint/function", async (HttpContext context, IResponseFormatter formatter) =>
                await formatter.Format(context, "Endpoint Function: It is sunny in LA"));

            #region tightly coupled middleware examples
            //// middleware function with response formatter example
            //IResponseFormatter formatter = new TextResponseFormatter();
            //app.MapGet("middleware/function", async (context) =>
            //    await formatter.Format(context, "Middleware Function: It is snowing in Chicago"));

            //// endpoint class example
            //app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);

            //// endpoint function example
            //app.MapGet("endpoint/function", async context =>
            //    await context.Response.WriteAsync("Endpoint Function: It is sunny in LA"));
            #endregion
            #region advanced routing examples
            //// accessing endpoint from middleware example
            //app.Use(async (context, next) =>
            //{
            //    Endpoint? endpoint = context.GetEndpoint();
            //    if (endpoint != null)
            //        await context.Response.WriteAsync($"{endpoint.DisplayName} Selected\n");
            //    else
            //        await context.Response.WriteAsync("No Endpoint Selected\n");

            //    await next();
            //});

            //// defined routing order and display name examples
            //app.Map("{number:int}", async context =>
            //    await context.Response.WriteAsync("Routed to INT endpoint"))
            //    .WithDisplayName("Int Endpoint")
            //    .Add(builder => ((RouteEndpointBuilder)builder).Order = 1);

            //app.Map("{number:double}", async context =>
            //    await context.Response.WriteAsync("Routed to DOUBLE endpoint"))
            //    .WithDisplayName("Double Endpoint")
            //    .Add(builder => ((RouteEndpointBuilder)builder).Order = 2);

            //// fallback endpoint example
            //app.MapFallback(async context =>
            //    await context.Response.WriteAsync("Routed to fallback endpoint."))
            //    .WithDisplayName("Fallback Endpoint");
            #endregion
            #region routing constraints examples
            //// constrained elements and catchall element
            //app.MapGet("{first:int}/{second:bool}/{*catchall}", async context =>
            //{
            //    await context.Response.WriteAsync("Request was routed\n");
            //    foreach (KeyValuePair<string, object?> kvp in context.Request.RouteValues)
            //        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
            //});

            //// custom constrained element
            //app.MapGet("capital/{country:countryName}", Capital.Endpoint);

            //// optional element
            //app.MapGet("population/{city?}", Population.Endpoint)
            //    .WithMetadata(new RouteNameMetadata("population"));
            #endregion
            #region configuration options examples
            //app.UseMiddleware<LocationMiddleWare>();

            //app.MapGet("/location", async (HttpContext context,
            //    IOptions<MessageOptions> msgOpts) =>
            //{
            //    MessageOptions options = msgOpts.Value;
            //    await context.Response.WriteAsync($"{options.CityName}, {options.CountryName}");
            //});
            #endregion
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

            app.Run();
        }
    }
}
