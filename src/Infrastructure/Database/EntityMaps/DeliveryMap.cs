using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Entities;

namespace Infrastructure.Database.EntityMaps;

public class DeliveryMap : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Deliveries", "mottu");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).IsRequired();
        builder.Property(m => m.Cnpj).IsRequired();
        builder.Property(m => m.CnhNumber).IsRequired();
        builder.Property(m => m.CnhType).IsRequired();

        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasConversion(new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            ));
    }
}