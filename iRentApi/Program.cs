using Data.Context;
using iRentApi.Helpers;
using iRentApi.Service;
using iRentApi.Service.Database;
using iRentApi.Service.Stripe;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200", "http://localhost:4201").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
});

app.MapControllers();

app.Run();
