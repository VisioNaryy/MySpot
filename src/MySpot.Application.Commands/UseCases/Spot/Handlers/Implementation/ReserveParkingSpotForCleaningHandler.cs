using MySpot.Data.EF.Repositories.Spots.Interfaces;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.UseCases.Reservation.Interfaces;
using MySpot.Domain.Services.UseCases.Reservation.Models;
using MySpot.Services.UseCases.Spot.Implementation;

namespace MySpot.Services.UseCases.Spot.Handlers.Implementation;

public sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
{
    private readonly IWeeklyParkingSpotRepository _repository;
    private readonly IParkingReservationService _reservationService;

    public ReserveParkingSpotForCleaningHandler(IWeeklyParkingSpotRepository repository, 
        IParkingReservationService reservationService)
    {
        _repository = repository;
        _reservationService = reservationService;
    }
    
    public async Task HandleAsync(ReserveParkingSpotForCleaning command)
    {
        var week = new Week(command.Date);
        var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();

        var request = new ReserveParkingForCleaningRequest(
            AllParkingSpots: weeklyParkingSpots, 
            Date: new Date(command.Date));

        _reservationService.ReserveParkingForCleaning(request);

        var tasks = weeklyParkingSpots.Select(x => _repository.UpdateAsync(x));
        await Task.WhenAll(tasks);
    }
}