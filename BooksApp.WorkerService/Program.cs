using BooksApp.Application.Abstractions.Repository;
using BooksApp.Application.Books.Commands;
using BooksApp.Jobs;
using BooksApp.Jobs.Abstractions;
using BooksApp.Persistence;
using BooksApp.Persistence.Implementations.Repositories;
using BooksApp.WorkerService;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

// Had to change from Host to Web application, so I can use Hangfire Dashboard
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();

builder.Services.AddScoped<IUpsertJob, UpsertJob>();

builder.Services.AddDbContext<BooksAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddMediatR(config =>
{
    // Gets any file from Application layer, just to get the correct Assembly
    // This should be done in some different way, but since the application is quite simple, I'll say this is fine
    config.RegisterServicesFromAssemblyContaining<UpsertBookCommand>();
});

builder.Services.AddHangfire(config =>
{
    config
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(
            builder.Configuration.GetConnectionString("Default"),
            new SqlServerStorageOptions
            {
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            });
});

builder.Services.AddHangfireServer();

builder.Services.AddHttpClient();

builder.WebHost.UseUrls("https://localhost:7162");

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseHangfireDashboard();

app.Run();
