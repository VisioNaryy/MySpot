using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MySpot.Data;
using MySpot.Data.EF.Contexts;
using MySpot.Data.EF.Repositories;
using MySpot.Domain.Data.IOptions;

namespace MySpot.Api.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        // options
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(SqlServerOptions.SectionName));

        AddSqlServer(services, configuration);
    }

    public static void AddSqlServer(this IServiceCollection services, ConfigurationManager configuration)
    {
        // contexts
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(SqlServerOptions.SectionName);
        services.AddDbContext<MySpotDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));
        
        // repositories
        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(IData))!)
            .AddClasses(c => c.AssignableTo(typeof(IRepository)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}