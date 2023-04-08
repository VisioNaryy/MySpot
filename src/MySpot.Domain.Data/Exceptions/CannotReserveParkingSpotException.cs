using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Data.Exceptions;

public sealed class CannotReserveParkingSpotException : BaseException
{
    public ParkingSpotId ParkingSpotId { get; }

    public CannotReserveParkingSpotException(ParkingSpotId parkingSpotId) 
        : base($"Cannot reserve parking spot with ID: {parkingSpotId} due to reservation policy.")
    {
        ParkingSpotId = parkingSpotId;
    }
}