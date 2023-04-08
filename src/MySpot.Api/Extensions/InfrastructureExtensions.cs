﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MySpot.Api.Middlewares;
using MySpot.Data;
using MySpot.Data.EF.Contexts;
using MySpot.Data.EF.Repositories;
using MySpot.Domain.Data.IOptions;

namespace MySpot.Api.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<ExceptionMiddleware>();
        
        AddSqlServer(services, configuration);
    }

    private static void AddSqlServer(this IServiceCollection services, ConfigurationManager configuration)
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
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
            reDoc.DocumentTitle = "MySpot API";
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        return app;
    }
}