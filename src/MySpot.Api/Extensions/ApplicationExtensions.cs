using System.Reflection;
using MySpot.Application.HostedServices.UseCases.Data.Implementation;
using MySpot.Domain.Data.IOptions;
using MySpot.Services;
using MySpot.Services.UseCases;
using MySpot.Services.UseCases.Domain.Implementation;
using MySpot.Services.UseCases.Domain.Interfaces;

namespace MySpot.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        // options
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(SqlServerOptions.SectionName));

        // services
        services.AddSingleton<IClock, Clock>();
        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(IServiceApp))!)
            .AddClasses(c => c.AssignableTo(typeof(IService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        // hosted services
        services.AddHostedService<DatabaseInitializer>();
    }
}