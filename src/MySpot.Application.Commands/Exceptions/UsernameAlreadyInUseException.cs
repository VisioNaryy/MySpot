using MySpot.Domain.Data.Exceptions;

namespace MySpot.Services.Exceptions;

public sealed class UsernameAlreadyInUseException : BaseException
{
    public string Username { get; }

    public UsernameAlreadyInUseException(string username) : base($"Username: '{username}' is already in use.")
    {
        Username = username;
    }
}