using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using UserMovieBooking.Domain.Repository.Interfaces;
using UserMovieBooking.Domain.Services.Interfaces;
using UserMovieBooking.Infrastructure.Contexts;
using UserMovieBooking.Infrastructure.Mapper;
using UserMovieBooking.Infrastructure.Repositories;
using UserMovieBooking.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("MovieDbContext") ?? string.Empty;

builder.Host.ConfigureAppConfiguration(configuration =>
{
    configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
}).UseSerilog();


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Add services to the container.
Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().Enrich.WithMachineName()
    .WriteTo.Debug().WriteTo.Console().WriteTo.Elasticsearch(
    new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticSearchConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM} "
    })
    .Enrich.WithProperty("Environment", environment)
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
// Add services to the container.

builder.Services.AddDbContext<MovieManagementContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITicketBookingRespository, TicketBookingRespository>();
builder.Services.AddScoped<ITicketBookingService, TicketBookingService>();

builder.Services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
    //etc
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = "JWT",
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
