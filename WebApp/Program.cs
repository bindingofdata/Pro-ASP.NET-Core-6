using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Antiforgery;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using WebApp.TagHelpers;
//using System.Text.Json.Serialization;

const string BASE_URL = "api/products";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<CitiesData>();
//builder.Services.AddTransient<ITagHelperComponent, TimeTagHelperComponent>();
//builder.Services.AddTransient<ITagHelperComponent, TableFooterTagHelperComponent>();

builder.Services.Configure<AntiforgeryOptions>(opts =>
    opts.HeaderName = "X-XSRF-TOKEN");

#region Razor Pages examples
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(opts =>
//    opts.Cookie.IsEssential = true);

//builder.Services.Configure<RazorPagesOptions>(opts =>
//    opts.Conventions.AddPageRoute("/Index", "/extra/page/{id:long?}"));
#endregion
#region XML formatting example
//builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();

//builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts =>
//    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
#endregion
#region Accept header support example
//builder.Services.Configure<MvcOptions>(opts =>
//{
//    opts.RespectBrowserAcceptHeader = true;
//    opts.ReturnHttpNotAcceptable = true;
//});
#endregion
#region OpenAPI/Swagger example
//builder.Services.AddSwaggerGen(setup =>
//    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp", Version = "v1" }));
#endregion
#region JSON PATCH example
//builder.Services.AddControllers().AddNewtonsoftJson();

//builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts =>
//    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
#endregion
#region Controller examples
//builder.Services.AddControllers();

// prevent all properties from being serialized when null
//builder.Services.Configure<JsonOptions>(opts =>
//    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
#endregion

var app = builder.Build();

app.UseStaticFiles();

IAntiforgery antiforgery = app.Services.GetRequiredService<IAntiforgery>();
app.Use(async (context, next) =>
{
    if (!context.Request.Path.StartsWithSegments("/api"))
    {
        string? token = antiforgery.GetAndStoreTokens(context).RequestToken;
        if (token != null)
        {
            context.Response.Cookies.Append("XSRF-TOKEN", token,
                new CookieOptions { HttpOnly = false});
        }
    }
    await next();
});

//app.MapControllers();
//app.MapDefaultControllerRoute();
app.MapControllerRoute("forms", "controllers/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

#region Razor Pages examples
//app.UseSession();
// convention routing example
//app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
#endregion
#region Controller examples
//app.MapControllers();
#endregion
#region OpenAPI/Swagger example
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp"));
#endregion
#region Minimal API examples
//// get a specific product
//app.MapGet($"{BASE_URL}/{{id}}", async (HttpContext context, DataContext data) =>
//{
//    string? id = context.Request.RouteValues["id"] as string;
//    if (!string.IsNullOrWhiteSpace(id))
//    {
//        Product? product = await data.Products.FindAsync(long.Parse(id));
//        if (product == null)
//            context.Response.StatusCode = StatusCodes.Status404NotFound;
//        else
//            await context.Response.WriteAsJsonAsync(product);
//    }
//});

//// get all products
//app.MapGet(BASE_URL, async (HttpContext context, DataContext data) =>
//    await context.Response.WriteAsJsonAsync(data.Products));

//// add a new product
//app.MapPost(BASE_URL, async (HttpContext context, DataContext data) =>
//{
//    Product? product = await context.Request.ReadFromJsonAsync<Product>();
//    if (product != null)
//    {
//        await data.AddAsync(product);
//        await data.SaveChangesAsync();
//        context.Response.StatusCode = StatusCodes.Status201Created;
//    }
//});
#endregion

DataContext context = app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();