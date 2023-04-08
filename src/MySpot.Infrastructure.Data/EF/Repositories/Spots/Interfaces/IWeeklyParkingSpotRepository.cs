using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Data.EF.Repositories.Spots.Interfaces;

public interface IWeeklyParkingSpotRepository : IRepository
{
    Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync();
    Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week) => throw new NotImplementedException();
    Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id);
    Task AddAsync(WeeklyParkingSpot weeklyParkingSpot);
    Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot);
}