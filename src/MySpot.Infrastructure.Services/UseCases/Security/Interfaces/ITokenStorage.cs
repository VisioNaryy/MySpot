using MySpot.Infrastructure.Services.UseCases.Security.Models;

namespace MySpot.Infrastructure.Services.UseCases.Security.Interfaces;

public interface ITokenStorage : IService
{
    void Set(JwtToken jwt);
    JwtToken? Get();
}