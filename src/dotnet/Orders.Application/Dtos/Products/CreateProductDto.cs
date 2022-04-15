using System.ComponentModel.DataAnnotations;

namespace Orders.Application.Dtos.Products;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public long Price { get; set; }

    [Required]
    public int AmountInStock { get; set; }
}