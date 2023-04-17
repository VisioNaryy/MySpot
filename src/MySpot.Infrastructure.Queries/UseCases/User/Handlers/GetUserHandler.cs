using Microsoft.EntityFrameworkCore;
using MySpot.Data.EF.Contexts;
using MySpot.Data.Extensions;
using MySpot.Domain.Data.Models;
using MySpot.Domain.Data.ValueObjects;
using MySpot.Infrastructure.Queries.UseCases.User.Queries;

namespace MySpot.Infrastructure.Queries.UseCases.User.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly MySpotDbContext _dbContext;
    
    public GetUserHandler(MySpotDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<UserDto?> HandleAsync(GetUser query)
    {
        var userId = new UserId(query.UserId);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}