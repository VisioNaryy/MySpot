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
using MySpot.Data.EF.UnitOfWork.Implementation;
using MySpot.Data.EF.UnitOfWork.Interfaces;
using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.IOptions;
using MySpot.Infrastructure.Queries;
using MySpot.Infrastructure.Queries.UseCases;
using MySpot.Infrastructure.Services;
using MySpot.Infrastructure.Services.UseCases;

namespace MySpot.Api.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration, IWebHostEnvironment environment)
    {
        var infrastructureQueriesAssembly = Assembly.GetAssembly(typeof(IInfrastructureQueriesApp))!;
        var infrastrucureServicesAssembly =  Assembly.GetAssembly(typeof(IInfrastructureServicesApp))!;
        
        AddSqlServer(services, configuration);
        AddAuth(services, configuration, environment);
        
        // Middleware
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();

        // Services
        services.Scan(s => s.FromAssemblies(infrastrucureServicesAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IService)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        // Query handlers
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
        // Contexts
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>(SqlServerOptions.SectionName);
        services.AddDbContext<MySpotDbContext>(x => x.UseSqlServer(sqlServerOptions.ConnectionString));

        // Repositories
        services.Scan(s => s.FromAssemblies(Assembly.GetAssembly(typeof(IInfrastructureDataApp))!)
            .AddClasses(c => c.AssignableTo(typeof(IRepository)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        // Unit of work
        services.AddScoped<IUnitOfWork, SqlServerUnitOfWork>();
    }

    private static void AddAuth(this IServiceCollection services, ConfigurationManager configuration, IWebHostEnvironment environment)
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
                o.IncludeErrorDetails = environment.IsDevelopment();
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
    
    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
            reDoc.DocumentTitle = "MySpot API";
        });
        
        app.MapControllers();
    }
}