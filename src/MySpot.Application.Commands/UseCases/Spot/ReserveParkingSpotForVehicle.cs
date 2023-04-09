namespace MySpot.Services.UseCases.Spot;

public sealed record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId, Guid UserId,
    string LicencePlate, int Capacity, DateTime Date) : ICommand;