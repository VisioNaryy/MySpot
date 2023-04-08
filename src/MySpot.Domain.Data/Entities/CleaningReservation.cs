using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Data.Entities;

public class CleaningReservation : Reservation
{
    public CleaningReservation(ReservationId id, Date date) : base(id, capacity: 2, date)
    {
    }
}