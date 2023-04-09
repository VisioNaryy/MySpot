using MySpot.Domain.Data.Exceptions;

namespace MySpot.Services.Exceptions;

public class UserNotFoundException : BaseException
{
    public Guid UserId { get; private set; }

    public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }
}