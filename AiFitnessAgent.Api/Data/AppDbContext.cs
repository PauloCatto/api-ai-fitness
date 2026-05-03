using AiFitnessAgent.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AiFitnessAgent.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("fitness");

        base.OnModelCreating(modelBuilder);
    }
}