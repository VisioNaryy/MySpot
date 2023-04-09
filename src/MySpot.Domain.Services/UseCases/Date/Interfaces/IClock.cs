namespace MySpot.Domain.Services.UseCases.Date.Interfaces;

public interface IClock : IDomainService
{
    DateTime Current();
}