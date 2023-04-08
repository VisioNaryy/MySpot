using MySpot.Comain.Data.ValueObjects;

namespace MySpot.Comain.Data.Entities;

public class CleaningReservation : Reservation
{
    public CleaningReservation(ReservationId id, Date date) : base(id, capacity: 2, date)
    {
    }
}