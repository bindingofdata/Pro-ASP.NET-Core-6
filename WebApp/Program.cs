using Microsoft.EntityFrameworkCore;

using WebApp;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});

var app = builder.Build();

app.UseMiddleware<TestMiddleware>();

app.MapGet("/", () => "Hello World!");

DataContext context = app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
