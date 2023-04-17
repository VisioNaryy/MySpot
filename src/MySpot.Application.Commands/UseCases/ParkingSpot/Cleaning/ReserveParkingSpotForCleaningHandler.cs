using MySpot.Data.EF.Repositories.Spots.Interfaces;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.UseCases.Reservation.Interfaces;
using MySpot.Domain.Services.UseCases.Reservation.Models;

namespace MySpot.Services.UseCases.ParkingSpot.Cleaning;

public sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
{
    private readonly IWeeklyParkingSpotRepository _repository;
    private readonly IParkingReservationService _reservationService;

    public ReserveParkingSpotForCleaningHandler(
        IWeeklyParkingSpotRepository repository, 
        IParkingReservationService reservationService)
    {
        _repository = repository;
        _reservationService = reservationService;
    }
    
    public async Task HandleAsync(ReserveParkingSpotForCleaning command)
    {
        var dateTimeOffset = command.Date;
        var week = new Week(dateTimeOffset);
        var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();

        var request = new ReserveParkingForCleaningRequest(
            AllParkingSpots: weeklyParkingSpots, 
            Date: new Date(dateTimeOffset));

        _reservationService.ReserveParkingForCleaning(request);

        var tasks = weeklyParkingSpots.Select(x => _repository.UpdateAsync(x));
        await Task.WhenAll(tasks);
    }
}