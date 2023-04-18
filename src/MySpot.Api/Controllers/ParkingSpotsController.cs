using Microsoft.AspNetCore.Mvc;
using MySpot.Domain.Data.Models;
using MySpot.Infrastructure.Queries.UseCases;
using MySpot.Infrastructure.Queries.UseCases.Spot.Get;
using MySpot.Services.UseCases;
using MySpot.Services.UseCases.ParkingSpot.Cleaning;
using MySpot.Services.UseCases.ParkingSpot.Vehicle;
using MySpot.Services.UseCases.Reservation.Change;
using MySpot.Services.UseCases.Reservation.Delete;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots")]
public class ParkingSpotsController : ControllerBase
{
    private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
        _getWeeklyParkingSpotsHandler;

    private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotForVehicleHandler;
    private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotForCleaningHandler;
    private readonly ICommandHandler<ChangeReservationLicencePlate> _changeReservationLicencePlateHandler;
    private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;

    public ParkingSpotsController(
        IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler,
        ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotForVehicleHandler,
        ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotForCleaningHandler,
        ICommandHandler<ChangeReservationLicencePlate> changeReservationLicencePlateHandler, 
        ICommandHandler<DeleteReservation> deleteReservationHandler)
    {
        _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;
        _reserveParkingSpotForVehicleHandler = reserveParkingSpotForVehicleHandler;
        _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
        _changeReservationLicencePlateHandler = changeReservationLicencePlateHandler;
        _deleteReservationHandler = deleteReservationHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
        => Ok(await _getWeeklyParkingSpotsHandler.HandleAsync(query));

    [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
    public async Task<IActionResult> Post(ReserveParkingSpotForVehicle command)
    {
        await _reserveParkingSpotForVehicleHandler.HandleAsync(command);

        return Ok();
    }
    
    [HttpPost("reservations/cleaning")]
    public async Task<IActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reserveParkingSpotForCleaningHandler.HandleAsync(command);

        return Ok();
    }
    
    [HttpPost("reservations/change")]
    public async Task<IActionResult> Post(ChangeReservationLicencePlate command)
    {
        await _changeReservationLicencePlateHandler.HandleAsync(command);

        return Ok();
    }
    
    [HttpPost("reservations/delete")]
    public async Task<IActionResult> Post(DeleteReservation command)
    {
        await _deleteReservationHandler.HandleAsync(command);

        return Ok();
    }
}