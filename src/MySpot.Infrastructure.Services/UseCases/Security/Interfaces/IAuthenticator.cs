using MySpot.Infrastructure.Services.UseCases.Security.Models;

namespace MySpot.Infrastructure.Services.UseCases.Security.Interfaces;

public interface IAuthenticator
{
    JwtToken CreateToken(Guid userId, string role);
}