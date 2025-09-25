using Infrastructure.Database.EntityMaps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Entities;

namespace Infrastructure.Database;

public class Context : DbContext
{
    public DbSet<Delivery> Deliverys { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<RentalPlan> RentalPlans { get; set; }
    public DbSet<RentalSubscription> RentalSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeliveryMap());
        modelBuilder.ApplyConfiguration(new MotorcycleMap());
        modelBuilder.ApplyConfiguration(new RentalPlanMap());
        modelBuilder.ApplyConfiguration(new RentalSubscriptionMap());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbconn = Environment.GetEnvironmentVariable("DBCONN_POSTGRES");

        optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        if (string.IsNullOrWhiteSpace(dbconn))
        {
            optionsBuilder.UseInMemoryDatabase("InMemory");
            return;
        }

        optionsBuilder.UseNpgsql(dbconn);
    }
}