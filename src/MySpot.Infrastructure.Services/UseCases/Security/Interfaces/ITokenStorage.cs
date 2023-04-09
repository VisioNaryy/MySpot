using MySpot.Infrastructure.Services.UseCases.Security.Models;

namespace MySpot.Infrastructure.Services.UseCases.Security.Interfaces;

public interface ITokenStorage
{
    void Set(JwtToken jwt);
    JwtToken Get();
}