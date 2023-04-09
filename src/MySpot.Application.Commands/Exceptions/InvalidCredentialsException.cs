using MySpot.Domain.Data.Exceptions;

namespace MySpot.Services.Exceptions;

public class InvalidCredentialsException : BaseException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}