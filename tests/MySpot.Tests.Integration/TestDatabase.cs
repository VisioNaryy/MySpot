using Microsoft.EntityFrameworkCore;
using MySpot.Data.EF.Contexts;
using MySpot.Domain.Data.IOptions;

namespace MySpot.Tests.Integrational;

internal sealed class TestDatabase : IDisposable
{
    public MySpotDbContext Context { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<SqlServerOptions>("SqlServer");
        Context = new MySpotDbContext(new DbContextOptionsBuilder<MySpotDbContext>().UseSqlServer(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}