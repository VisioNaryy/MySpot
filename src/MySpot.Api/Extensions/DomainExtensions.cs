using MySpot.Domain.Data.IOptions;

namespace MySpot.Api.Extensions;

public static class DomainExtensions
{
    public static void AddDomain(this IServiceCollection services, ConfigurationManager configuration)
    {
        // options
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(SqlServerOptions.SectionName));
        services.Configure<AppOptions>(configuration.GetRequiredSection(AppOptions.SectionName));
    }
}