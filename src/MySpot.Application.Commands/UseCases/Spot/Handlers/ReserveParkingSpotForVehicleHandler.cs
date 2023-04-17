using MySpot.Data.EF.Repositories.Spots.Interfaces;
using MySpot.Data.EF.Repositories.Users.Interfaces;
using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.UseCases.Date.Interfaces;
using MySpot.Domain.Services.UseCases.Reservation.Interfaces;
using MySpot.Domain.Services.UseCases.Reservation.Models;
using MySpot.Services.Exceptions;
using MySpot.Services.UseCases.Spot.Commands;

namespace MySpot.Services.UseCases.Spot.Handlers;

public sealed class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
{
    private readonly IWeeklyParkingSpotRepository _repository;
    private readonly IParkingReservationService _reservationService;
    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;

    public ReserveParkingSpotForVehicleHandler(
        IWeeklyParkingSpotRepository repository, 
        IParkingReservationService reservationService, 
        IUserRepository userRepository,
        IClock clock)
    {
        _repository = repository;
        _reservationService = reservationService;
        _userRepository = userRepository;
        _clock = clock;
    }

    public async Task HandleAsync(ReserveParkingSpotForVehicle command)
    {
        var (spotId, reservationId, userId, licencePlate, capacity, date) = command;
        var week = new Week(_clock.Current());
        var parkingSpotId = new ParkingSpotId(spotId);
        var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();
        var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        
        if (parkingSpotToReserve is null)
        {
            throw new WeeklyParkingSpotNotFoundException(spotId);
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }

        var reservation = new VehicleReservation(
            reservationId: reservationId, 
            userId: user.Id, 
            employeeName: new EmployeeName(user.FullName),
            licencePlate: licencePlate, 
            capacity: capacity, 
            date: new Date(date));

        var request = new ReserveSpotForVehicleRequest(
            AllParkingSpots: weeklyParkingSpots, 
            JobTitle: JobTitle.Employee,
            ParkingSpotToReserve: parkingSpotToReserve, 
            Reservation: reservation);

        _reservationService.ReserveSpotForVehicle(request);
        
        await _repository.UpdateAsync(parkingSpotToReserve);
    }
}