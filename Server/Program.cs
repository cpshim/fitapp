using FitApp.Identity.Data;
using FitApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Adds Identity Minimal API endpoints (no UI). Depends on additional middleware for cookies.
// Limited; uses prebuilt Identity API endpoints. No custom controllers needed (API-based).
// builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//     .AddEntityFrameworkStores<UserDbContext>();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("AppDb"));

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddApiEndpoints();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "TestCookie";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Identity/Account/Login";
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Used for AddIdentityApiEndpoints service when user model not extending the Identity User class
// app.MapIdentityApi<IdentityUser>();

app.MapIdentityApi<User>();

app.MapControllers();

app.Run();
