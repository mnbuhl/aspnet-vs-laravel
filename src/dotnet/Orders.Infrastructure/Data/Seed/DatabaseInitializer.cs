using Microsoft.EntityFrameworkCore;
using Orders.Domain.Models;

namespace Orders.Infrastructure.Data.Seed;

public static class DatabaseInitializer
{
    private static readonly Random Random = new Random();
    public static void Seed(ModelBuilder modelBuilder)
    {
        var users = new List<User>();

        for (int i = 0; i < 100; i++)
        {
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = $"user{i}@email.com",
                Name = $"User {i}",
                Phone = Random.Next(20000000, 99999999).ToString()
            });
        }
        
        modelBuilder.Entity<User>().HasData(users);

        var products = new List<Product>();

        for (int i = 0; i < 150; i++)
        {
            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = $"Product {i}",
                Description = $"Description for Product {i}",
                Price = i == 0 ? 1 : i * 1000,
                AmountInStock = Random.Next(1000, 10000)
            });
        }
        
        modelBuilder.Entity<Product>().HasData(products);

        var address = new Address
        {
            Id = Guid.NewGuid(),
            AddressLine = $"Test Rd 100",
            City = "Test City",
            Country = "Test Country",
            ZipCode = 5555
        };
        modelBuilder.Entity<Address>().HasData(address);

        var shippingDetailsList = new List<ShippingDetails>();
        var orders = new List<Order>();

        foreach (var user in users)
        {
            int orderCount = Random.Next(0, 20);

            for (int i = 0; i < orderCount; i++)
            {
                var shippingDetails = new ShippingDetails
                {
                    Id = Guid.NewGuid(),
                    Carrier = "Test carrier",
                    ShippedAt = DateTime.UtcNow.AddDays(Random.Next(1, 10)),
                    DeliveredAt = Random.Next(0, 10) > 7 ? DateTime.UtcNow.AddDays(Random.Next(10, 20)) : null,
                    ShippingAddressId = address.Id,
                };
                
                shippingDetailsList.Add(shippingDetails);
                
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    UserId = user.Id,
                    BillingAddressId = address.Id,
                    ShippingDetailsId = shippingDetails.Id
                };
                
                orders.Add(order);
            }
        }

        modelBuilder.Entity<ShippingDetails>().HasData(shippingDetailsList);

        var orderLines = new List<OrderLine>();

        foreach (var order in orders)
        {
            int productSeed = Random.Next(0, 150);
            
            for (int i = 0; i < Random.Next(1, 5); i++)
            {
                var orderLine = new OrderLine
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = products[productSeed].Id,
                    Price = products[productSeed].Price,
                    Quantity = Random.Next(1, 5),
                    Discount = Random.Next(0, 10) > 7 ? Random.Next(1, 100) : 0
                };
                
                order.OrderLines.Add(orderLine);
                orderLines.Add(orderLine);
            }
            
            order.CalculateTotal();
            order.OrderLines = new List<OrderLine>();
        }

        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<Order>().OwnsMany(o => o.OrderLines).HasData(orderLines);
    }
}