using Microsoft.AspNetCore.Http;
using MySpot.Infrastructure.Services.UseCases.Security.Interfaces;
using MySpot.Infrastructure.Services.UseCases.Security.Models;

namespace MySpot.Infrastructure.Services.UseCases.Security.Implementation;

internal sealed class HttpContextTokenStorage : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(JwtToken jwt)
    {
        _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);
    }

    public JwtToken Get()
    {
        if (_httpContextAccessor.HttpContext is null) return null;

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt)) return jwt as JwtToken;

        return null;
    }
}