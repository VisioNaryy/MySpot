using MySpot.Domain.Data.Exceptions;

namespace MySpot.Services.Exceptions;

public sealed class EmailAlreadyInUseException : BaseException
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}