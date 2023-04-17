using Microsoft.AspNetCore.Mvc;
using MySpot.Domain.Data.Models;
using MySpot.Infrastructure.Queries.UseCases;
using MySpot.Infrastructure.Queries.UseCases.Spot.Get;
using MySpot.Services.UseCases;
using MySpot.Services.UseCases.ParkingSpot.Vehicle;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots")]
public class ParkingSpotsController : ControllerBase
{
    private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
        _getWeeklyParkingSpotsHandler;

    private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotForVehicleHandler;

    public ParkingSpotsController(
        IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler,
        ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotForVehicleHandler)
    {
        _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;
        _reserveParkingSpotForVehicleHandler = reserveParkingSpotForVehicleHandler;
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
}