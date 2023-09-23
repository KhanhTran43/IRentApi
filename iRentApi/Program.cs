using Data.Context;
using iRentApi.Helpers;
using iRentApi.Service.Database;
using iRentApi.Service.Stripe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var secret = builder.Configuration.GetSection("AppSettings")["Secret"];
builder.Services.AddControllers();
builder.Services.AddDbContext<IRentContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("IRentDB"));
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<StripeService>();
StripeConfiguration.ApiKey = "sk_test_51Moi0JC3H9WnnPLnb7SMrdWlrHU0SeReC003pwjYGykDNTtFUH7ykplqfy4huQrKMT17YPYgmUaINBT4GEbNC9BC006OLHU3r5";
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200", "http://localhost:4201").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
