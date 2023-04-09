namespace MySpot.Domain.Data.Exceptions;

public sealed class InvalidRoleException : BaseException
{
    public string Role { get; }

    public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
    {
        Role = role;
    }
}