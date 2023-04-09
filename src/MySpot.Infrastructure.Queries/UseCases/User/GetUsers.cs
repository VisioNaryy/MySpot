using MySpot.Domain.Data.Models;

namespace MySpot.Infrastructure.Queries.UseCases.User;

public class GetUsers : IQuery<IEnumerable<UserDto>>
{
}