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
            Product = orderLine.Product.ToDto(),
            Discount = orderLine.Discount,
        };
    }
}