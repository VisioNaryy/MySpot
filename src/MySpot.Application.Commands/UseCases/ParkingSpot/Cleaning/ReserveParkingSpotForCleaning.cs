namespace MySpot.Services.UseCases.ParkingSpot.Cleaning;

public record ReserveParkingSpotForCleaning(DateTime Date) : ICommand;