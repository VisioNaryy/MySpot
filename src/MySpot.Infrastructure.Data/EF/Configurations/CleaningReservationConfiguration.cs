using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Domain.Data.Entities;

namespace MySpot.Data.EF.Configurations;

public sealed class CleaningReservationConfiguration : IEntityTypeConfiguration<CleaningReservation>
{
    public void Configure(EntityTypeBuilder<CleaningReservation> builder)
    {
    }
}