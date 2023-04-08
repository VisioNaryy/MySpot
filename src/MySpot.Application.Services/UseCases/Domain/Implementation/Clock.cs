using MySpot.Services.UseCases.Domain.Interfaces;

namespace MySpot.Services.UseCases.Domain.Implementation;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}