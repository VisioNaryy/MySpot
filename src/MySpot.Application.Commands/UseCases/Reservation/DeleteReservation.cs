namespace MySpot.Services.UseCases.Reservation;

public sealed record DeleteReservation(Guid ReservationId) : ICommand;