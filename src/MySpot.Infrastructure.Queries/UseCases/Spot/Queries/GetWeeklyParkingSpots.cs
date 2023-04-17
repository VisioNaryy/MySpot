using MySpot.Domain.Data.Models;

namespace MySpot.Infrastructure.Queries.UseCases.Spot.Queries;

public class GetWeeklyParkingSpots : IQuery<IEnumerable<WeeklyParkingSpotDto>>
{
    public DateTime? Date { get; set; }
}