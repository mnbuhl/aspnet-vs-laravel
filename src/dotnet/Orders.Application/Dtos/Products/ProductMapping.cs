using Orders.Domain.Models;

namespace Orders.Application.Dtos.Products;

public static class ProductMapping
{
    public static ProductDto? ToDto(this Product? product)
    {
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            AmountInStock = product.AmountInStock
        };
    }
}