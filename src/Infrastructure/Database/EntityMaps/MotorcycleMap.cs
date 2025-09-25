using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace Infrastructure.Database.EntityMaps;

public class MotorcycleMap : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.ToTable("Motorcycles", "mottu");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Year).IsRequired();
        builder.Property(m => m.Model).IsRequired();
        builder.Property(m => m.Plate).IsRequired();
    }
}