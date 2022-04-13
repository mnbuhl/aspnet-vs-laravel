using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orders.Domain.Models;
using Orders.Infrastructure.Data.Seed;

namespace Orders.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLine> OrderLines => Set<OrderLine>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ShippingDetails> ShippingDetails => Set<ShippingDetails>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ShippingDetails>().Navigation(s => s.ShippingAddress).AutoInclude();
        DatabaseInitializer.Seed(modelBuilder);
    }
}