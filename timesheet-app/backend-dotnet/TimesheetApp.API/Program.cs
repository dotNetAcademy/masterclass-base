using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TimesheetApp.Application.ErrorHandler;
using TimesheetApp.Application.Interfaces.Repositories;
using TimesheetApp.Application.Queries.Employees;
using TimesheetApp.Infrastructure;
using TimesheetApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services
  .AddAuthorization(options =>
  {
      options.AddPolicy(
        "read:employees",
        policy => policy.Requirements.Add(new HasScopeRequirement("read:employees", builder.Configuration["Auth0:Domain"]!)));
      options.AddPolicy(
        "read:registrations",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("read:registrations", builder.Configuration["Auth0:Domain"]!)));
      options.AddPolicy(
        "write:registrations",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("write:registrations", builder.Configuration["Auth0:Domain"]!)));
      options.AddPolicy(
        "read:timesheets",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("read:timesheets", builder.Configuration["Auth0:Domain"]!)));
      options.AddPolicy(
        "submit:timesheets",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("submit:timesheets", builder.Configuration["Auth0:Domain"]!)));
      options.AddPolicy(
        "approve:timesheets",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("approve:timesheets", builder.Configuration["Auth0:Domain"]!)));
      options.AddPolicy(
        "read:holidays",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("read:holidays", builder.Configuration["Auth0:Domain"]!)));
      options.AddPolicy(
        "write:holidays",
        policy => policy.Requirements.Add(
          new HasScopeRequirement("write:holidays", builder.Configuration["Auth0:Domain"]!)));
  });

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())).EnableSensitiveDataLogging()
    .UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetEmployeesQuery>());

builder.Services.AddControllers();

// configure DI for application services
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();
builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();

var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/Logging.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactFrontEnd", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactFrontEnd");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

public partial class Program
{
}
