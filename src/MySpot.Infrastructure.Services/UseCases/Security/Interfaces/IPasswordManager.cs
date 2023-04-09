namespace MySpot.Infrastructure.Services.UseCases.Security.Interfaces;

public interface IPasswordManager : IService
{
    string Secure(string password);
    bool Validate(string password, string securedPassword);
}