namespace MySpot.Services.UseCases.Reservation.Change;

public sealed record ChangeReservationLicencePlate(Guid ReservationId, string LicencePlate) : ICommand;