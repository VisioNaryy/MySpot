namespace MySpot.Domain.Data.Exceptions;

public sealed class InvalidEmailException : BaseException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}