namespace Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("countryName", typeof(CountryRouteConstraint)));

            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                Endpoint? endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    await context.Response.WriteAsync($"{endpoint.DisplayName} Selected\n");
                }
                else
                {
                    await context.Response.WriteAsync("No Endpoint Selected\n");
                }
                await next();
            });

            app.Map("{number:int}", async context =>
                await context.Response.WriteAsync("Routed to INT endpoint"))
                .WithDisplayName("Int Endpoint")
                .Add(builder => ((RouteEndpointBuilder)builder).Order = 1);

            app.Map("{number:double}", async context =>
                await context.Response.WriteAsync("Routed to DOUBLE endpoint"))
                .WithDisplayName("Double Endpoint")
                .Add(builder => ((RouteEndpointBuilder)builder).Order = 2);

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

            app.MapFallback(async context =>
                await context.Response.WriteAsync("Routed to fallback endpoint."))
                .WithDisplayName("Fallback Endpoint");

            app.Run();
        }
    }
}
