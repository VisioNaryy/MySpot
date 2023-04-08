using MySpot.Comain.Data.ValueObjects;

namespace MySpot.Comain.Data.Entities;

public abstract class Reservation
{
    public ReservationId Id { get; }
    public Capacity Capacity { get; private set; }
    public Date Date { get; private set; }

    protected Reservation()
    {
    }

    protected Reservation(ReservationId id, Capacity capacity, Date date)
    {
        Id = id;
        Capacity = capacity;
        Date = date;
    }
}