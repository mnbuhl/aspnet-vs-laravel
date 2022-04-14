using Orders.Application.Dtos.OrderLines;
using Orders.Application.Dtos.Users;
using Orders.Domain.Models;

namespace Orders.Application.Dtos.Orders;

public static class OrderMappings
{
    public static OrderDto? ToDto(this Order? order, bool includeUser = true)
    {
        if (order == null)
        {
            return null;
        }

        return new OrderDto
        {
            Id = order.Id,
            Date = order.Date,
            Total = order.Total,
            BillingAddress = order.BillingAddress,
            OrderLines = order.OrderLines.Select(x => x.ToDto()).ToList(),
            ShippingDetails = order.ShippingDetails,
            User = includeUser ? order.User.ToDto() : null,
        };
    }
}