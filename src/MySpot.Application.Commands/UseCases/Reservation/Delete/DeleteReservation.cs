namespace MySpot.Services.UseCases.Reservation.Delete;

public sealed record DeleteReservation(Guid ReservationId) : ICommand;