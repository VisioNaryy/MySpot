using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.Policies.Interfaces;

namespace MySpot.Domain.Services.Policies.Implementation;

internal sealed class BossReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Boss;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        => true;
}