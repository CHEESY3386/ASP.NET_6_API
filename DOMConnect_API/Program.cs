global using Serilog;

using DOMConnect_API;
using DOMConnect_API.CustomConstraints;
using DOMConnect_API.Database;
using DOMConnect_API.Middleware;
using DOMConnect_API.Services.Config;
using DOMConnect_API.Services.Token;
using DOMConnect_API.Utilities.Extensions;
using DOMConnect_API.Redis;
using DOMConnect_API.IO;

using Microsoft.OpenApi.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

#if DEBUG

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .Add<WritableJsonConfigurationSource>(s =>
    {
        s.FileProvider = null;
        s.Path = $"appsettings.json";
        s.ReloadOnChange = true;
        s.Optional = false;
        s.ResolveFileProvider();
    })
    .Add<WritableJsonConfigurationSource>(s =>
    {
        s.FileProvider = null;
        s.Path = $"appsettings.{env}.json";
        s.ReloadOnChange = true;
        s.Optional = true;
        s.ResolveFileProvider();
    })
    .AddEnvironmentVariables()
    .Build();

#else

builder.WebHost.UseUrls("http://0.0.0.0:8080");

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .Add<WritableJsonConfigurationSource>(s =>
    {
        s.FileProvider = null;
        s.Path = $"/dombox/conf/appsettings.json";
        s.ReloadOnChange = true;
        s.Optional = false;
        s.ResolveFileProvider();
    })
    .AddEnvironmentVariables()
    .Build();

#endif

// Logging
builder.WebHost.ConfigureLogging(logging =>
{
    logging.ClearProviders(); // removes asp.net logs
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

// Configuration

builder.Services.AddSingleton<IConfigurationRoot>(config);

// Controllers
builder.Services.AddCors();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            return new ValidationFailedResult(actionContext.ModelState);
        };
    });

// DB Context
builder.Services.AddDbContextFactory<MySqlContext>(lifetime: ServiceLifetime.Singleton);

// Redis
builder.Services.AddSingleton<RedisContext>();

// Services
builder.Services.AddScoped<ConfigurationService>();
builder.Services.AddScoped<TokenService>();

// Custom Constraints
builder.Services.Configure<RouteOptions>(routeOptions =>
{
    routeOptions.ConstraintMap.Add("ipv4", typeof(IPv4Constraint));
});

// Json naming policy settings
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = new CamelCaseNamingPolicy(),
    WriteIndented = true
};

// Lowercase endpoint enforcement
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Extra Debug features
#if DEBUG
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.OperationFilter<SecureEndpointAuthRequirementFilter>();
});
#endif

// Main application
var app = builder.Build();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>(); // always keep this middleware registered first
app.UseWhen(
    httpContext => !httpContext.Request.Path.StartsWithSegments("/oauth2") 
    && !httpContext.Request.Path.StartsWithSegments("/swagger"),
    configuration => configuration.UseOAuth2Middleware()); // exclude OAuth2 middleware from /oauth2 and /swagger routes

app.MapControllers();


// Swagger
#if DEBUG
app.UseSwagger();
app.UseSwaggerUI();
#endif

// Run
app.Run();
