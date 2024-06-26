using System.Text.Json;
using System.Text.Json.Serialization;
using CapitalPlacementProj.Common.Extensions;
using CapitalPlacementProj.Common.Middleware;
using CapitalPlacementProj.Configuration;
using FastEndpoints.Swagger;
using Microsoft.Azure.Cosmos;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
builder
    .Configuration.AddUserSecrets<Program>() // used to manage secrets during development
    .AddEnvironmentVariables()
    .AddJsonFile($"appsettings.{builder.Environment}.json", optional: true, reloadOnChange: true)
    .Build();

var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .WriteTo.Console(
        LogEventLevel.Verbose,
        "[{Timestamp:HH:mm:ss}] [{Level:u3}] {SourceContext}: {Message}{NewLine}{Exception}"
    )
    .CreateLogger();

logger.Information("Starting up");

try
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.AddServerHeader = false;
        options.AllowSynchronousIO = false;
    });

    if (builder.Environment.IsProduction())
    {
        builder.Logging.ClearProviders();
    }

    builder.Services.AddSingleton(logger);
    builder.Services.SetupConfigFiles();

    builder.Host.UseConsoleLifetime(options => options.SuppressStatusMessages = true);

    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    builder.Services.AddFastEndpoints(opt =>
    {
        opt.Assemblies = [typeof(Program).Assembly];
    });

    builder.Services.SwaggerDocument(opt =>
    {
        opt.EnableJWTBearerAuth = false;
    });

    // register azure cosmos client
    builder.Services.AddSingleton(provider =>
    {
        return new CosmosClient(
            builder.Configuration.GetConnectionString("CosmosDB"),
            new CosmosClientOptions
            {
                SerializerOptions = new()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            }
        );
    });

    builder.Services.RegisterServices();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseFastEndpoints(options =>
    {
        options.Errors.ResponseBuilder = (errors, _, _) => errors.ToResponse();
        options.Errors.StatusCode = StatusCodes.Status422UnprocessableEntity;
        options.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.Serializer.Options.ReadCommentHandling = JsonCommentHandling.Skip;
        options.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwaggerUi(x => x.ConfigureDefaults());
    }

    // todo: add serilog http request logging
    await app.RunAsync();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Unhandled exception");
}
finally
{
    logger.Error("Shut down complete");
    Log.CloseAndFlush();
}

public partial class Program;
