namespace Orders.Application.Dtos.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long Price { get; set; }
    public int AmountInStock { get; set; }
    public string Description { get; set; } = string.Empty;
}