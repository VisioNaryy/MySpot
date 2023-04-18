using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySpot.Domain.Data.IOptions;
using MySpot.Domain.Services.UseCases.Date.Interfaces;
using MySpot.Infrastructure.Services.UseCases.Security.Interfaces;
using MySpot.Infrastructure.Services.UseCases.Security.Models;

namespace MySpot.Infrastructure.Services.UseCases.Security.Implementation;

public sealed class Authenticator : IAuthenticator
{
    private readonly string _audience;
    private readonly IClock _clock;
    private readonly TimeSpan _expiry;
    private readonly string _issuer;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();
    private readonly SigningCredentials _signingCredentials;

    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
            SecurityAlgorithms.HmacSha512);
    }

    public JwtToken CreateToken(Guid userId, string role)
    {
        var now = _clock.Current();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new()
        {
            AccessToken = token
        };
    }
}