namespace MySpot.Services.UseCases.Reservation;

public sealed record ChangeReservationLicencePlate(Guid ReservationId, string LicencePlate) : ICommand;