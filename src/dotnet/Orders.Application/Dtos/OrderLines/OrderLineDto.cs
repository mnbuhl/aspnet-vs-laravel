using Orders.Domain.Models;

namespace Orders.Application.Dtos.OrderLines;

public class OrderLineDto
{
    public Guid Id { get; set; }
    public long Price { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public Product? Product { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}