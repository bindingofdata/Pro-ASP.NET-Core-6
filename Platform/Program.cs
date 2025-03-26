using Platform.Services;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.FileProviders;

namespace Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Caching data example
            //// In memory cache (not actually distributed)
            //builder.Services.AddDistributedMemoryCache(opts =>
            //    opts.SizeLimit = 200);

            // SQL server distributed cache
            builder.Services.AddDistributedSqlServerCache(opts =>
            {
                opts.ConnectionString = builder.Configuration["ConnectionStrings:CacheConnection"];
                opts.SchemaName = "dbo";
                opts.TableName = "DataCache";
            });

            // Caching responses
            builder.Services.AddResponseCaching();
            builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
            #endregion
            #region Configuring the Session Service and Middleware
            //builder.Services.AddDistributedMemoryCache();
            //builder.Services.AddHttpsRedirection(opts =>
            //{
            //    opts.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    opts.HttpsPort = 5500;
            //});
            //builder.Services.AddSession(opts =>
            //{
            //    opts.IdleTimeout = TimeSpan.FromMinutes(30);
            //    opts.Cookie.IsEssential = true;
            //});
            #endregion
            #region Enabling HTTP Strict Transport Security
            //builder.Services.AddHsts(opts =>
            //{
            //    opts.MaxAge = TimeSpan.FromDays(1);
            //    opts.IncludeSubDomains = true;
            //});
            #endregion
            #region Enabling Cookie Consent Checking
            //builder.Services.Configure<CookiePolicyOptions>(opts =>
            //    opts.CheckConsentNeeded = context => true);
            #endregion
            #region Logging HTTP requests and responses example
            //builder.Services.AddHttpLogging(opts =>
            //{
            //    opts.LoggingFields = HttpLoggingFields.RequestMethod
            //        | HttpLoggingFields.RequestPath
            //        | HttpLoggingFields.ResponseStatusCode;
            //});
            #endregion
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

            #region caching data example
            // Caching responses
            app.UseResponseCaching();

            app.MapEndpoint<SumEndpoint>("/sum/{count:int=1000000000}");
            #endregion
            #region handling Exceptions and Errors example
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/error.html");
            //    app.UseStaticFiles();
            //}

            //app.UseStatusCodePages("text/html", ResponseString.DefaultResponse);

            //app.Use(async (context, next) =>
            //{
            //    if (string.Equals("/error", context.Request.Path))
            //    {
            //        context.Response.StatusCode = StatusCodes.Status404NotFound;
            //        await Task.CompletedTask;
            //    }
            //    else
            //    {
            //        await next();
            //    }
            //});

            //app.Run(context =>
            //    throw new Exception("Something has gone wrong"));
            #endregion
            #region enabling HTTP Strict Transport Security
            //if (app.Environment.IsProduction())
            //{
            //    app.UseHsts();
            //}

            //app.MapFallback(async context =>
            //{
            //    await context.Response.WriteAsync($"HTTPS Request: {context.Request.IsHttps}\n");
            //    await context.Response.WriteAsync("Hello World!");
            //});
            #endregion
            #region configuring the Session Service and Middleware
            //app.UseHttpsRedirection();
            //app.UseSession();
            //app.UseMiddleware<ConsentMiddleware>();
            #endregion
            #region using Session data example
            //app.MapGet("/session", async context =>
            //{
            //    int counter1 = (context.Session.GetInt32("counter1") ?? 0) + 1;
            //    int counter2 = (context.Session.GetInt32("counter2") ?? 0) + 1;
            //    context.Session.SetInt32("counter1", counter1);
            //    context.Session.SetInt32("counter2", counter2);
            //    await context.Session.CommitAsync();
            //    await context.Response.WriteAsync($"Counter1: {counter1}, Counter2: {counter2}");
            //});
            #endregion
            #region enabling cookie consent checking
            //app.UseCookiePolicy();
            //app.UseMiddleware<ConsentMiddleware>();
            #endregion
            #region using cookies example
            //app.MapGet("/cookie", async context =>
            //{
            //    int counter1 = int.Parse(context.Request.Cookies["counter1"] ?? "0") + 1;
            //    context.Response.Cookies.Append("counter1", counter1.ToString(),
            //        new CookieOptions
            //        {
            //            MaxAge = TimeSpan.FromMinutes(30),
            //            IsEssential = true
            //        });

            //    int counter2 = int.Parse(context.Request.Cookies["counter2"] ?? "0") + 1;
            //    context.Response.Cookies.Append("counter2", counter2.ToString(),
            //        new CookieOptions { MaxAge = TimeSpan.FromMinutes(30) });

            //    await context.Response.WriteAsync($"Counter1: {counter1}, Counter2: {counter2}");
            //});

            //app.MapGet("clear", context =>
            //{
            //    context.Response.Cookies.Delete("counter1");
            //    context.Response.Cookies.Delete("counter2");
            //    context.Response.Redirect("/");
            //    return Task.CompletedTask;
            //});
            #endregion
            #region using static content and client-side packages example
            //app.UseHttpLogging();

            //app.UseStaticFiles();

            //IWebHostEnvironment env = app.Environment;
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider($"{env.ContentRootPath}/staticfiles"),
            //    RequestPath = "/files"
            //});

            //app.MapGet("population/{city?}", Population.Endpoint);
            #endregion
            #region logging HTTP requests and responses examples
            //app.UseHttpLogging();
            //app.MapGet("population/{city?}", Population.Endpoint);
            #endregion
            #region generating logging messages examples
            //var logger = app.Services.GetRequiredService<ILoggerFactory>()
            //    .CreateLogger("Pipeline");
            //logger.LogDebug("Pipeline configuration starting");

            //app.MapGet("population/{city?}", Population.Endpoint);

            //logger.LogDebug("Pipeline configuration complete");
            #endregion
            #region reading user secrets example
            //app.MapGet("config", async (HttpContext context, IConfiguration config) =>
            //{
            //    string wsID = config["WebService:Id"];
            //    string wsKey = config["WebService:Key"];
            //    await context.Response.WriteAsync($"\nThe Secret ID is: {wsID}");
            //    await context.Response.WriteAsync($"\nThe Secret Key is: {wsKey}");
            //});
            #endregion
            #region chapter 14 code
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
