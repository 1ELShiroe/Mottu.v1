using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace Infrastructure.Database.EntityMaps;

public class RentalPlanMap : IEntityTypeConfiguration<RentalPlan>
{
    public void Configure(EntityTypeBuilder<RentalPlan> builder)
    {
        builder.ToTable("RentalPlans", "mottu");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.DailyCost).IsRequired();
        builder.Property(m => m.DurationInDays).IsRequired();
    }
}