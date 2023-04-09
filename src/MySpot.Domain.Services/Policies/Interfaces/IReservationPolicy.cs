using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Services.Policies.Interfaces;

public interface IReservationPolicy
{
    bool CanBeApplied(JobTitle jobTitle);
    bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName);
}