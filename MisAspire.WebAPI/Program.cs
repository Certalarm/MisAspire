
using log4net;
using log4net.Config;
using MisAspire.Persistence.Context;
using MisAspire.WebAPI.Exceptions;
using System.Reflection;

namespace MisAspire.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddLog4Net();

        // Load log4net config
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly()!);
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        builder.AddServiceDefaults();
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddDbContext<MisContext>();
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseExceptionHandler();
        app.MapControllers();

        app.Run();
    }
}
