using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orders.Domain.Models;

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
}