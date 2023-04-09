using System.Reflection;
using MySpot.Domain.Data.IOptions;
using MySpot.Domain.Services;
using MySpot.Domain.Services.Policies.Interfaces;
using MySpot.Domain.Services.UseCases;
using MySpot.Domain.Services.UseCases.Date.Implementation;
using MySpot.Domain.Services.UseCases.Date.Interfaces;

namespace MySpot.Api.Extensions;

public static class DomainExtensions
{
    public static void AddDomain(this IServiceCollection services, ConfigurationManager configuration)
    {
        var domainServiceAppAssembly = Assembly.GetAssembly(typeof(IDomainServiceApp))!;
        
        // options
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(SqlServerOptions.SectionName));
        services.Configure<AppOptions>(configuration.GetRequiredSection(AppOptions.SectionName));
        
        // policies
        services.Scan(s => s.FromAssemblies(domainServiceAppAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IReservationPolicy)))
            .AsSelf()
            .WithSingletonLifetime());
        
        // services
        services.Scan(s => s.FromAssemblies(domainServiceAppAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IDomainService)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }
}