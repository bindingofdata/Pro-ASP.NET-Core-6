//using Platform.Services;

namespace Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Unbound types in services example
            //builder.Services.AddSingleton(typeof(ICollection<>), typeof(List<>));
            #endregion
            #region Services with multiple implementations examples
            //builder.Services.AddScoped<IResponseFormatter, TextResponseFormatter>();
            //builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
            //builder.Services.AddScoped<IResponseFormatter, GuidService>();
            #endregion
            #region Service Factory Functions example
            //IConfiguration config = builder.Configuration;
            //builder.Services.AddScoped<IResponseFormatter>(serviceProvider =>
            //{
            //    string? typeName = config["services:IResponseFormatter"];
            //    return (IResponseFormatter)ActivatorUtilities.CreateInstance(
            //        serviceProvider, string.IsNullOrWhiteSpace(typeName)
            //            ? typeof(GuidService) : Type.GetType(typeName, true)!);
            //});
            #endregion
            #region Accessing services in the Program.cs file
            //IWebHostEnvironment env = builder.Environment;
            //if (env.IsDevelopment())
            //{
            //    builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
            //    builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();
            //}
            //else
            //{
            //    builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
            //}
            #endregion
            #region Dependency chains example
            //// Start of chain example
            //builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
            //// Second item in chain example: Time Response Formatter references ITimeStamper
            //builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();
            #endregion
            #region Service lifetime examples
            // Singleton service example
            //builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();

            // Transient service example
            //builder.Services.AddTransient<IResponseFormatter, GuidService>();

            // Scoped service example
            //builder.Services.AddScoped<IResponseFormatter, GuidService>();
            #endregion

            var app = builder.Build();

            #region Chapter 14 code
            //app.UseMiddleware<WeatherMiddleware>();
            #endregion
            #region unbound types in services examples
            //app.MapGet("string", async context =>
            //{
            //    ICollection<string> collection = context.RequestServices
            //        .GetRequiredService<ICollection<string>>();
            //    collection.Add($"Request: {DateTime.Now.ToLongTimeString()}");
            //    foreach (string str in collection)
            //        await context.Response.WriteAsync($"String: {str}\n");
            //});

            //app.MapGet("int", async context =>
            //{
            //    ICollection<int> collection = context.RequestServices
            //        .GetRequiredService<ICollection<int>>();
            //    collection.Add(collection.Count + 1);
            //    foreach (int val in collection)
            //        await context.Response.WriteAsync($"Int: {val}\n");
            //});
            #endregion
            #region services with multiple implementations example
            //app.MapGet("single", async context =>
            //{
            //    IResponseFormatter formatter = context.RequestServices
            //        .GetRequiredService<IResponseFormatter>();
            //    await formatter.Format(context, "Single service");
            //});

            //app.MapGet("/", async context =>
            //{
            //    IResponseFormatter formatter = context.RequestServices
            //        .GetServices<IResponseFormatter>().First(f => f.RichOutput);
            //    await formatter.Format(context, "Multiple services");
            //});
            #endregion
            #region basic dependency injection examples
            //// Dependency Injection middleware example
            //app.MapGet("middleware/function", async (HttpContext context, IResponseFormatter formatter) =>
            //    await formatter.Format(context, "Middleware Function: It is snowing in Chicago"));

            //// Dependency Injection in endpoint class example
            ////app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);
            //app.MapEndpoint<WeatherEndpoint>("endpoint/class");

            //// Dependency Injection endpoint example
            //// Updated to use scoped service
            //app.MapGet("endpoint/function", async (HttpContext context) =>
            //{
            //    IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
            //    await formatter.Format(context, "Endpoint Function: It is sunny in LA");
            //});
            #endregion
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

            app.MapGet("/", async context =>
                await context.Response.WriteAsync("Hello World!"));

            app.Run();
        }
    }
}
