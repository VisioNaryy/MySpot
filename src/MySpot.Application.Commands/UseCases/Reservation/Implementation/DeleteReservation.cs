namespace MySpot.Services.UseCases.Reservation.Implementation;

public sealed record DeleteReservation(Guid ReservationId) : ICommand;