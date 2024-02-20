using FlightPlannerWebApi.Handlers;
using FlightPlannerWebApi.Interfaces;
using FlightPlannerWebApi.Models;
using FlightPlannerWebApi.Storage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<IFlightService, InMemoryFlightStorage>();
builder.Services.AddScoped<IFlightService, DatabaseFlightStorage>();

builder.Services.AddDbContext<FlightDbContext>(option =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

    option.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();