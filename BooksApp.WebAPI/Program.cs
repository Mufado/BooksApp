using BooksApp.Application.Abstractions.Repository;
using BooksApp.Application.Books.Commands;
using BooksApp.Persistence;
using BooksApp.Persistence.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// I like to fix the routes as lowercase inside the application for consistency reasons
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();

builder.Services.AddDbContext<BooksAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IBooksRepository, BooksRepository>();

builder.Services.AddMediatR(config =>
{
    // Gets any file from Application layer, just to get the correct Assembly
    // This probably should be done in some different way, but since this solution structure is quite simple, I'll say this is fine
    config.RegisterServicesFromAssemblyContaining<UpsertBookCommand>();
});

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRequests",
        builder =>
        {
            builder.WithOrigins(allowedOrigins!)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("AllowRequests");

app.Run();
