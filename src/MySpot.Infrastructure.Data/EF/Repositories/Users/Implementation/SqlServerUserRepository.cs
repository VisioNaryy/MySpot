using Microsoft.EntityFrameworkCore;
using MySpot.Data.EF.Contexts;
using MySpot.Data.EF.Repositories.Users.Interfaces;
using MySpot.Domain.Data.Entities;
using MySpot.Domain.Data.ValueObjects;

namespace MySpot.Data.EF.Repositories.Users.Implementation;

public sealed class SqlServerUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public SqlServerUserRepository(MySpotDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public Task<User?> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User?> GetByUsernameAsync(Username username)
        => _users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);
}