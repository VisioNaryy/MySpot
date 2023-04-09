namespace MySpot.Domain.Data.Exceptions;

public sealed class InvalidLicencePlateException : BaseException
{
    public string LicencePlate { get; }

    public InvalidLicencePlateException(string licencePlate) 
        : base($"Licence plate: {licencePlate} is invalid.")
    {
        LicencePlate = licencePlate;
    }
}