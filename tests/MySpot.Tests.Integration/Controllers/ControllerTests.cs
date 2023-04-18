using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySpot.Domain.Data.IOptions;
using MySpot.Domain.Services.UseCases.Date.Implementation;
using MySpot.Infrastructure.Services.UseCases.Security.Interfaces;
using MySpot.Infrastructure.Services.UseCases.Security.Models;
using MySpot.Infrastructure.Services.UseCases.Security.Implementation;

namespace MySpot.Tests.Integrational.Controllers;

[Collection("api")]
public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    private readonly IAuthenticator _authenticator;
    protected HttpClient Client { get; }

    protected JwtToken Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }

    public ControllerTests(OptionsProvider optionsProvider)
    {
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOptions), new Clock());
        var app = new MySpotTestApp(ConfigureServices);
        Client = app.Client;
    }
    
    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }
}