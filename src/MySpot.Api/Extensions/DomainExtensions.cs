using System.Reflection;
using MySpot.Domain.Data.IOptions;
using MySpot.Domain.Services;
using MySpot.Domain.Services.Policies.Interfaces;
using MySpot.Domain.Services.UseCases;

namespace MySpot.Api.Extensions;

public static class DomainExtensions
{
    public static void AddDomain(this IServiceCollection services, ConfigurationManager configuration)
    {
        var domainServiceAppAssembly = Assembly.GetAssembly(typeof(IDomainServiceApp))!;
        
        // Options
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(SqlServerOptions.SectionName));
        services.Configure<AppOptions>(configuration.GetRequiredSection(AppOptions.SectionName));
        services.Configure<AuthOptions>(configuration.GetRequiredSection(AuthOptions.SectionName));
        
        // Policies
        services.Scan(s => s.FromAssemblies(domainServiceAppAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IReservationPolicy)))
            .AsSelf()
            .WithSingletonLifetime());
        
        // Services
        services.Scan(s => s.FromAssemblies(domainServiceAppAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IDomainService)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }
}