using MySpot.Domain.Data.Exceptions;

namespace MySpot.Services.Exceptions;

public sealed class ReservationNotFoundException : BaseException
{
    public Guid Id { get; }

    public ReservationNotFoundException(Guid id) 
        : base($"Reservation with ID: {id} was not found.")
    {
        Id = id;
    }
}