namespace Orders.Application.Dtos.Products;

public class UpdateProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? Price { get; set; }
    public int? AmountInStock { get; set; }
}