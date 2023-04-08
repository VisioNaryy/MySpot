using Microsoft.EntityFrameworkCore;
using MySpot.Domain.Data.Entities;

namespace MySpot.Data.EF.Contexts;

public sealed class MySpotDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }
    public DbSet<User> Users { get; set; }

    public MySpotDbContext(DbContextOptions<MySpotDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}