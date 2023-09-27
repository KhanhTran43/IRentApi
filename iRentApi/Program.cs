using Data.Context;
using Hangfire;
using Hangfire.SqlServer;
using iRentApi.Helpers;
using iRentApi.Job;
using iRentApi.Service.Database;
using iRentApi.Service.Database.Contract;
using iRentApi.Service.Stripe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var appSettings = builder.Configuration.GetSection("AppSettings");
var secret = appSettings["Secret"];
var useDb = appSettings["UseDb"];
builder.Services.AddControllers();
builder.Services.AddDbContext<IRentContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString(useDb));
});

// Add scan job
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseColouredConsoleLogProvider();
    config.UseSqlServerStorage(
                 builder.Configuration.GetConnectionString(useDb),
        new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true,
        });

    var server = new BackgroundJobServer(new BackgroundJobServerOptions
    {
        ServerName = "hangfire-test",
    });
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<StripeService>();
//builder.Services.AddScoped<UpdateRentedWarehouseStatusJob>();
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
    options.WithOrigins("http://localhost:4200", "http://localhost:4201", "https://irent-gamma.vercel.app", "https://irent-admin.vercel.app/#/warehouse").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

var timezone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Ho_Chi_Minh");

BackgroundJob.Enqueue<UpdateRentedWarehouseStatusJob>(x => x.Execute());
RecurringJob.AddOrUpdate<UpdateRentedWarehouseStatusJob>(
    "daily-update-rented-warehouse-status-job",
    x => x.Execute(),
    "0 0 * * *",
    new RecurringJobOptions()
    {
        TimeZone = timezone
    }
);

app.Run();
