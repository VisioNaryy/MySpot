namespace MySpot.Services.UseCases.Reservation.Implementation;

public sealed record ChangeReservationLicencePlate(Guid ReservationId, string LicencePlate) : ICommand;