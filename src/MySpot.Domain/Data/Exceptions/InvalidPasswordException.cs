namespace MySpot.Domain.Data.Exceptions;

public sealed class InvalidPasswordException : BaseException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}