using Microsoft.EntityFrameworkCore;

using WebApp;
using WebApp.Models;

const string BASE_URL = "api/products";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});

var app = builder.Build();

app.MapGet($"{BASE_URL}/{{id}}", async (HttpContext context, DataContext data) =>
{
    string? id = context.Request.RouteValues["id"] as string;
    if (!string.IsNullOrWhiteSpace(id))
    {
        Product? product = await data.Products.FindAsync(long.Parse(id));
        if (product == null)
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        else
            await context.Response.WriteAsJsonAsync(product);
    }
});

app.MapGet(BASE_URL, async (HttpContext context, DataContext data) =>
    await context.Response.WriteAsJsonAsync(data.Products));

app.MapPost(BASE_URL, async (HttpContext context, DataContext data) =>
{
    Product? product = await context.Request.ReadFromJsonAsync<Product>();
    if (product != null)
    {
        await data.AddAsync(product);
        await data.SaveChangesAsync();
        context.Response.StatusCode = StatusCodes.Status201Created;
    }
});

app.MapGet("/", () => "Hello World!");

DataContext context = app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();