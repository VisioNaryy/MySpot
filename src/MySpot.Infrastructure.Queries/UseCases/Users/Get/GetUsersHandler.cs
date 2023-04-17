using Microsoft.EntityFrameworkCore;
using MySpot.Data.EF.Contexts;
using MySpot.Data.Extensions;
using MySpot.Domain.Data.Models;

namespace MySpot.Infrastructure.Queries.UseCases.Users.Get;

internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly MySpotDbContext _dbContext;

    public GetUsersHandler(MySpotDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        => await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}