using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySpot.Api.Middlewares;
using MySpot.Data;
using MySpot.Data.EF.Contexts;
using MySpot.Data.EF.Repositories;
using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.IOptions;
using MySpot.Infrastructure.Queries;
using MySpot.Infrastructure.Queries.UseCases;
using MySpot.Infrastructure.Services;
using MySpot.Infrastructure.Services.UseCases;
using MySpot.Services.UseCases;

namespace MySpot.Api.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var infrastructureQueriesAssembly = Assembly.GetAssembly(typeof(IInfrastructureQueriesApp))!;
        var infrastrucureServicesAssembly =  Assembly.GetAssembly(typeof(IInfrastructureServicesApp))!;
        
        AddSqlServer(services, configuration);
        AddAuth(services, configuration);
        
        // middleware
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();

        // services
        services.Scan(s => s.FromAssemblies(infrastrucureServicesAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IService)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        // query handlers
        services.Scan(s => s.FromAssemblies(infrastructureQueriesAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MySpot API",
                Version = "v1"
            });
        });
    }

    private static void AddSqlServer(this IServiceCollection services, ConfigurationManager configuration)
    {
        // contexts
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(SqlServerOptions.SectionName);
        services.AddDbContext<MySpotDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));

        // repositories
        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(IInfrastructureDataApp))!)
            .AddClasses(c => c.AssignableTo(typeof(IRepository)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    private static void AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var options = configuration.GetOptions<AuthOptions>(AuthOptions.SectionName);
        
        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Audience = options.Audience;
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
            });

        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddAuthorization(authorization =>
        {
            authorization.AddPolicy("is-admin", policy =>
            {
                policy.RequireRole("admin");
            });
        });
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