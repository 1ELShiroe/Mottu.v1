using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace Infrastructure.Database.EntityMaps;

public class RentalSubscriptionMap : IEntityTypeConfiguration<RentalSubscription>
{
    public void Configure(EntityTypeBuilder<RentalSubscription> builder)
    {
        builder.ToTable("RentalSubscriptions", "mottu");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.DeliveryId).IsRequired();
        builder.Property(r => r.MotorcycleId).IsRequired();
        builder.Property(r => r.RentalPlanId).IsRequired();

        builder.Property(r => r.StartDate).IsRequired();
        builder.Property(r => r.EndDate).IsRequired();
        builder.Property(r => r.ExpectedEndDate).IsRequired();

        builder.Property(r => r.DailyCost).IsRequired().HasColumnType("decimal(10,2)");
        builder.Property(r => r.TotalCost).IsRequired().HasColumnType("decimal(10,2)");

        builder.HasOne(r => r.Plan)
               .WithMany()
               .HasForeignKey(r => r.RentalPlanId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Motorcycle)
               .WithMany()
               .HasForeignKey(r => r.MotorcycleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Delivery)
               .WithMany()
               .HasForeignKey(r => r.DeliveryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
