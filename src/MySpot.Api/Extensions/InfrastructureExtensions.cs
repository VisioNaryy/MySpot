using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MySpot.Application.HostedServices.UseCases.Data.Implementation;
using MySpot.Data;
using MySpot.Data.EF.Contexts;
using MySpot.Data.EF.Repositories;
using MySpot.Domain.Data.IOptions;
using MySpot.Services;
using MySpot.Services.UseCases;
using MySpot.Services.UseCases.Domain.Implementation;
using MySpot.Services.UseCases.Domain.Interfaces;

namespace MySpot.Api.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        AddSqlServer(services, configuration);
    }

    public static void AddSqlServer(this IServiceCollection services, ConfigurationManager configuration)
    {
        // contexts
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(SqlServerOptions.SectionName);
        services.AddDbContext<MySpotDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));

        // repositories
        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(IDataApp))!)
            .AddClasses(c => c.AssignableTo(typeof(IRepository)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}