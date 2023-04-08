namespace MySpot.Comain.Data.Exceptions;

public class InvalidUsernameException : CustomException
{
    public string UserName { get; }

    public InvalidUsernameException(string userName) : base($"Username: '{userName}' is invalid.")
    {
        UserName = userName;
    }
}