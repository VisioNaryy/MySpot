using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Data.Exceptions;

public sealed class ParkingSpotCapacityExceededException : BaseException
{
    public ParkingSpotId ParkingSpotId { get; }

    public ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId) 
        : base($"Parking spot with ID: {parkingSpotId} exceeded its reservation capacity.")
    {
        ParkingSpotId = parkingSpotId;
    }
}