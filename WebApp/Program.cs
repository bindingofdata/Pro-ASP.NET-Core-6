using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Newtonsoft.Json;
//using System.Text.Json.Serialization;

const string BASE_URL = "api/products";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});

#region JSON PATCH example
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts =>
    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
#endregion
#region Controller examples
//builder.Services.AddControllers();

// prevent all properties from being serialized when null
//builder.Services.Configure<JsonOptions>(opts =>
//    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
#endregion

var app = builder.Build();

#region Controller examples
app.MapControllers();
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

app.MapGet("/", () => "Hello World!");

DataContext context = app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();