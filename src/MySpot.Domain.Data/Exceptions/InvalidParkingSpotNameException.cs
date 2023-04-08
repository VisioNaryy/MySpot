namespace MySpot.Comain.Data.Exceptions;

public class InvalidParkingSpotNameException : CustomException
{
    public InvalidParkingSpotNameException() : base("Parking spot name is invalid.")
    {
    }
}