using Orders.Application.Dtos.Products;

namespace Orders.Application.Dtos.OrderLines;

public class OrderLineDto
{
    public Guid Id { get; set; }
    public long Price { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto? Product { get; set; }
}