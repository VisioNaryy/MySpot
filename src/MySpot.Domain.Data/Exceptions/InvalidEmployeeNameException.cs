namespace MySpot.Domain.Data.Exceptions;

public sealed class InvalidEmployeeNameException : BaseException
{
    public InvalidEmployeeNameException() : base("Employee name is invalid.")
    {
    }
}