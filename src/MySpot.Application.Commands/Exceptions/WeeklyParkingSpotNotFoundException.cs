using MySpot.Domain.Data.Exceptions;

namespace MySpot.Services.Exceptions;

public sealed class WeeklyParkingSpotNotFoundException : BaseException
{
    public Guid? Id { get; }

    public WeeklyParkingSpotNotFoundException() 
        : base("Weekly parking spot with ID was not found.")
    {
    }

    public WeeklyParkingSpotNotFoundException(Guid id) 
        : base($"Weekly parking spot with ID: {id} was not found.")
    {
        Id = id;
    }
}