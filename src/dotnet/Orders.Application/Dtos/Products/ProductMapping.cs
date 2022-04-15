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

    public static Product ToDomain(this CreateProductDto productDto)
    {
        return new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            AmountInStock = productDto.AmountInStock
        };
    }

    public static Product MapUpdateDto(this Product product, UpdateProductDto productDto)
    {
        product.Name = productDto.Name ?? product.Name;
        product.Description = productDto.Description ?? product.Description;
        product.Price = productDto.Price ?? product.Price;
        product.AmountInStock = productDto.AmountInStock ?? product.AmountInStock;

        return product;
    }
}