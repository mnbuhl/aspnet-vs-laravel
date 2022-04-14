using Orders.Application.Dtos.Addresses;
using Orders.Application.Dtos.OrderLines;
using Orders.Application.Dtos.ShippingDetail;
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
            BillingAddress = order.BillingAddress.ToDto(),
            OrderLines = order.OrderLines.Select(x => x.ToDto()).ToList(),
            ShippingDetails = order.ShippingDetails.ToDto(),
            User = includeUser ? order.User.ToDto() : null,
        };
    }
}