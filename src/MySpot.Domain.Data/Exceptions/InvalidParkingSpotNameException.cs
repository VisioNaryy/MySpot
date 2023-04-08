namespace MySpot.Domain.Data.Exceptions;

public class InvalidParkingSpotNameException : BaseException
{
    public InvalidParkingSpotNameException() : base("Parking spot name is invalid.")
    {
    }
}