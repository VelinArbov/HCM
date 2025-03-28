using HCM.ApiService.Endpoints;
using HCM.Domain.Models;
using HCM.Domain.Models.Application.Interfaces;
using HCM.Infrastructure.Configuration;
using HCM.Infrastructure.Data;
using HCM.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
var dbSettings = builder.Configuration
    .GetSection("ConnectionStrings")
    .Get<DatabaseSettings>();

builder.Services.AddDbContext<HcmDbContext>(options =>
    options.UseSqlServer(dbSettings!.DefaultConnection));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<HcmDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login"; // for Blazor redirects
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

builder.Services.AddScoped<IPeopleService, PeopleService>();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.MapDefaultEndpoints();
app.MapPeopleEndpoints();
app.MapAuthEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.Run();

