using Microsoft.AspNetCore.Builder;
using Serilog;

namespace MySpot.Logging.Extensions;

public static class LoggerExtensions
{
    public static void UseSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationIdHeader()
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}