using MySpot.Domain.Services.UseCases.Date.Interfaces;

namespace MySpot.Domain.Services.UseCases.Date.Implementation;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}