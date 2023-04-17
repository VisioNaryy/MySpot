using MySpot.Data.EF.Repositories.Spots.Interfaces;
using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Services.Exceptions;

namespace MySpot.Services.UseCases.Reservation.Change;

public sealed class ChangeReservationLicencePlateHandler : ICommandHandler<ChangeReservationLicencePlate>
{
    private readonly IWeeklyParkingSpotRepository _repository;

    public ChangeReservationLicencePlateHandler(IWeeklyParkingSpotRepository repository)
        => _repository = repository;

    public async Task HandleAsync(ChangeReservationLicencePlate command)
    {
        var (guid, licencePlate) = command;
        
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservation(guid);
        if (weeklyParkingSpot is null)
        {
            throw new WeeklyParkingSpotNotFoundException();
        }

        var reservationId = new ReservationId(guid);
        var reservation = weeklyParkingSpot.Reservations
            .OfType<VehicleReservation>()
            .SingleOrDefault(x => x.Id == reservationId);

        if (reservation is null)
        {
            throw new ReservationNotFoundException(guid);
        }
    
        reservation.ChangeLicencePlate(licencePlate);
        await _repository.UpdateAsync(weeklyParkingSpot);
    }
    
    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(ReservationId id)
        => (await _repository.GetAllAsync())
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
}