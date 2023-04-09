using MySpot.Domain.Data.Entities;

namespace MySpot.Domain.Services.UseCases.Reservation.Models;

public record ReserveParkingForCleaningRequest(
    IEnumerable<WeeklyParkingSpot> AllParkingSpots, 
    Data.ValueObjects.Date Date);