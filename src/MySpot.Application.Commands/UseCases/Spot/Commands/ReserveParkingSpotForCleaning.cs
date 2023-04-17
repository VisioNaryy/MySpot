namespace MySpot.Services.UseCases.Spot.Commands;

public record ReserveParkingSpotForCleaning(DateTime Date) : ICommand;