using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Domain.Services.Policies.Interfaces;

namespace MySpot.Domain.Services.Policies.Implementation;

internal sealed class ManagerReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Manager;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
            .SelectMany(x => x.Reservations)
            .OfType<VehicleReservation>()
            .Count(x => x.EmployeeName == employeeName);

        return totalEmployeeReservations <= 4;
    }
}