using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Domain.Data.Entities;

public abstract class Reservation
{
    public ReservationId Id { get; }
    public Capacity Capacity { get; }
    public Date Date { get; }

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