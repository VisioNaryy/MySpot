namespace MySpot.Domain.Data.Exceptions;

public class InvalidUsernameException : BaseException
{
    public string UserName { get; }

    public InvalidUsernameException(string userName) : base($"Username: '{userName}' is invalid.")
    {
        UserName = userName;
    }
}