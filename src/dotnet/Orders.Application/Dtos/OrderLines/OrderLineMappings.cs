using Orders.Domain.Models;

namespace Orders.Application.Dtos.OrderLines;

public static class OrderLineMappings
{
    public static OrderLineDto ToDto(this OrderLine orderLine)
    {
        return new OrderLineDto
        {
            Id = orderLine.Id,
            Price = orderLine.Price,
            Quantity = orderLine.Quantity,
            Product = orderLine.Product,
            Discount = orderLine.Discount,
            CreatedAt = orderLine.CreatedAt,
            UpdatedAt = orderLine.UpdatedAt
        };
    }
}