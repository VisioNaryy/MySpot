using MySpot.Domain.Services.UseCases.Reservation.Models;

namespace MySpot.Domain.Services.UseCases.Reservation.Interfaces;

public interface IParkingReservationService : IDomainService
{
    void ReserveSpotForVehicle(ReserveSpotForVehicleRequest request);

    void ReserveParkingForCleaning(ReserveParkingForCleaningRequest request);
}