using System.Reflection;
using MySpot.Application.HostedServices.UseCases.Data.Implementation;
using MySpot.Domain.Data.IOptions;
using MySpot.Services;
using MySpot.Services.UseCases;

namespace MySpot.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var serviceAppAssembly = Assembly.GetAssembly(typeof(IServiceApp))!;
        
        // options
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(SqlServerOptions.SectionName));

        // services
        services.Scan(s => s.FromAssemblies(serviceAppAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IService)))
            .AsSelf()
            .WithScopedLifetime());
        
        // hosted services
        services.AddHostedService<DatabaseInitializer>();
    }
}