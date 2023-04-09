using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.Exceptions;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.Policies.Interfaces;
using MySpot.Domain.Services.UseCases.Date.Interfaces;
using MySpot.Domain.Services.UseCases.Reservation.Interfaces;
using MySpot.Domain.Services.UseCases.Reservation.Models;

namespace MySpot.Domain.Services.UseCases.Reservation.Implementation;

public sealed class ParkingReservationService : IParkingReservationService
{
    private readonly IEnumerable<IReservationPolicy> _policies;
    private readonly IClock _clock;

    public ParkingReservationService(IEnumerable<IReservationPolicy> policies, IClock clock)
    {
        _policies = policies;
        _clock = clock;
    }

    public void ReserveSpotForVehicle(ReserveSpotForVehicleRequest request)
    {
        var (allParkingSpots, jobTitle, parkingSpotToReserve, reservation) = request;
        
        var parkingSpotId = parkingSpotToReserve.Id;
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
        {
            throw new NoReservationPolicyFoundException(jobTitle);
        }

        if (!policy.CanReserve(allParkingSpots, reservation.EmployeeName))
        {
            throw new CannotReserveParkingSpotException(parkingSpotId);
        }

        parkingSpotToReserve.AddReservation(reservation, new Data.ValueObjects.Date(_clock.Current()));
    }

    public void ReserveParkingForCleaning(ReserveParkingForCleaningRequest request)
    {
        var (allParkingSpots, date) = request;
        
        foreach (var parkingSpot in allParkingSpots)
        {
            var reservationsForSameDate = parkingSpot.Reservations.Where(x => x.Date == date);
            parkingSpot.RemoveReservations(reservationsForSameDate);
            parkingSpot.AddReservation(new CleaningReservation(ReservationId.Create(), date),
                new Data.ValueObjects.Date(_clock.Current()));
        }
    }
}