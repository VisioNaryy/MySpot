using MySpot.Data.EF.Contexts;
using MySpot.Data.EF.UnitOfWork.Interfaces;

namespace MySpot.Data.EF.UnitOfWork.Implementation;

public sealed class SqlServerUnitOfWork : IUnitOfWork
{
    private readonly MySpotDbContext _mySpotDbContext;

    public SqlServerUnitOfWork(MySpotDbContext mySpotDbContext)
        => _mySpotDbContext = mySpotDbContext;

    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        await using var transaction = await _mySpotDbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await _mySpotDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}