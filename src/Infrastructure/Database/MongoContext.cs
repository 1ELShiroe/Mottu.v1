using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Shared.Entities;

namespace Infrastructure.Database;

public sealed class MongoContext : DbContext
{
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>().ToCollection("logs");

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbconn = Environment.GetEnvironmentVariable("DBCONN_MONGOOSE");

        if (string.IsNullOrWhiteSpace(dbconn))
        {
            optionsBuilder.UseInMemoryDatabase("InMemory");
        }

        optionsBuilder.UseMongoDB(dbconn!);
    }
}