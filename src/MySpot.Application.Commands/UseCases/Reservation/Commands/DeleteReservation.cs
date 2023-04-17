namespace MySpot.Services.UseCases.Reservation.Commands;

public sealed record DeleteReservation(Guid ReservationId) : ICommand;