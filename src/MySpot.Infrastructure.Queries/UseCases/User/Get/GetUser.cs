using MySpot.Domain.Data.Models;

namespace MySpot.Infrastructure.Queries.UseCases.User.Get;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}