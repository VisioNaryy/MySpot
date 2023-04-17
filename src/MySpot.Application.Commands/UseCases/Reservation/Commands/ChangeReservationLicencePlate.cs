namespace MySpot.Services.UseCases.Reservation.Commands;

public sealed record ChangeReservationLicencePlate(Guid ReservationId, string LicencePlate) : ICommand;