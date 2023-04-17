using System.Reflection;
using MySpot.Application.HostedServices.UseCases.Data.Implementation;
using MySpot.Domain.Data.IOptions;
using MySpot.Services;
using MySpot.Services.Decorators;
using MySpot.Services.UseCases;

namespace MySpot.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var commandsAppAssembly = Assembly.GetAssembly(typeof(ICommandApp))!;
        
        // options
        services.Configure<SqlServerOptions>(configuration.GetRequiredSection(SqlServerOptions.SectionName));

        // command handlers
        services.Scan(s => s.FromAssemblies(commandsAppAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // hosted services
        services.AddHostedService<DatabaseInitializer>();
        
        // decorators
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
    }
}