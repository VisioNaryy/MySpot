using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Services.UseCases.Reservation.Models;

public record ReserveSpotForVehicleRequest(
    IEnumerable<WeeklyParkingSpot> AllParkingSpots, 
    JobTitle JobTitle,
    WeeklyParkingSpot ParkingSpotToReserve, 
    VehicleReservation Reservation);