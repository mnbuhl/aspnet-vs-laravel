using Orders.Application.Dtos.Products;
using Orders.Domain.Models;

namespace Orders.Application.Dtos.OrderLines;

public static class OrderLineMappings
{
    public static OrderLineDto? ToDto(this OrderLine? orderLine)
    {
        if (orderLine == null)
            return null;

        return new OrderLineDto
        {
            Id = orderLine.Id,
            Price = orderLine.Price,
            Quantity = orderLine.Quantity,
            ProductId = orderLine.ProductId,
            Product = orderLine.Product.ToDto(),
            Discount = orderLine.Discount,
        };
    }

    public static OrderLine ToDomain(this CreateOrderLineDto orderLineDto)
    {
        return new OrderLine
        {
            Price = orderLineDto.Price,
            Quantity = orderLineDto.Quantity,
            ProductId = orderLineDto.ProductId,
            Discount = orderLineDto.Discount,
        };
    }
}