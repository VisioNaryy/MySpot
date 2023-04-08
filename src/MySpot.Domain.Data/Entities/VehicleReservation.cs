using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Data.Entities;

public class VehicleReservation : Reservation
{
    public UserId UserId { get; }
    public EmployeeName EmployeeName { get; }
    public LicencePlate LicencePlate { get; private set; }

    private VehicleReservation()
    {
    }

    public VehicleReservation(ReservationId reservationId, UserId userId, EmployeeName employeeName,
        LicencePlate licencePlate, Capacity capacity, Date date) : base(reservationId, capacity, date)
    {
        UserId = userId;
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
    }

    public void ChangeLicencePlate(LicencePlate licencePlate)
        => LicencePlate = licencePlate;
}