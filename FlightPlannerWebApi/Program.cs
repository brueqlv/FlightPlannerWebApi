using System.Reflection;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validators;
using FlightPlanner.Data;
using FlightPlanner.Services;
using FlightPlanner.UseCases;
using FlightPlanner.WebApi.Handlers;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FlightDbContext>(option =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

    option.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);

builder.Services.AddTransient<IFlightDbContext, FlightDbContext>();
builder.Services.AddTransient<IDbService, DbService>();
builder.Services.AddTransient<IEntityService<Airport>, EntityService<Airport>>();
builder.Services.AddTransient<IEntityService<Flight>, EntityService<Flight>>();
builder.Services.AddTransient<IFlightService, FlightService>();
builder.Services.AddTransient<IAirportService, AirportService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(typeof(FlightValidator).Assembly);

builder.Services.AddServices();

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