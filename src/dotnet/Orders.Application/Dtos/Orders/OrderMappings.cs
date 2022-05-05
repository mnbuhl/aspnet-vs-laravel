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
            User = includeUser ? order.User.ToDto(false) : null,
        };
    }

    public static Order ToDomain(this CreateOrderDto orderDto)
    {
        return new Order
        {
            Id = orderDto.Id ?? Guid.NewGuid(),
            Date = orderDto.Date,
            UserId = orderDto.UserId,
            BillingAddressId = orderDto.BillingAddressId,
            BillingAddress = orderDto.BillingAddress?.ToDomain(),
            ShippingDetails = orderDto.ShippingDetails?.ToDomain(),
            OrderLines = orderDto.OrderLines.Select(ol => ol.ToDomain()).ToList()
        };
    }
}