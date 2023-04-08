using MySpot.Comain.Data.ValueObjects;

namespace MySpot.Comain.Data.Exceptions;

public sealed class ParkingSpotCapacityExceededException : CustomException
{
    public ParkingSpotId ParkingSpotId { get; }

    public ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId) 
        : base($"Parking spot with ID: {parkingSpotId} exceeded its reservation capacity.")
    {
        ParkingSpotId = parkingSpotId;
    }
}