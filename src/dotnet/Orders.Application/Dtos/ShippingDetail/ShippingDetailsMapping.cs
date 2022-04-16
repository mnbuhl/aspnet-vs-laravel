using Orders.Application.Dtos.Addresses;
using Orders.Domain.Models;

namespace Orders.Application.Dtos.ShippingDetail;

public static class ShippingDetailsMapping
{
    public static ShippingDetailsDto? ToDto(this ShippingDetails? shippingDetails)
    {
        if (shippingDetails == null)
            return null;

        return new ShippingDetailsDto
        {
            Id = shippingDetails.Id,
            Carrier = shippingDetails.Carrier,
            ShippingAddress = shippingDetails.ShippingAddress.ToDto(),
            ShippedAt = shippingDetails.ShippedAt,
            DeliveredAt = shippingDetails.DeliveredAt
        };
    }

    public static ShippingDetails ToDomain(this CreateShippingDetailsDto shippingDetailsDto)
    {
        return new ShippingDetails
        {
            Carrier = shippingDetailsDto.Carrier,
            ShippingAddressId = shippingDetailsDto.ShippingAddressId,
            ShippingAddress = shippingDetailsDto.ShippingAddress?.ToDomain(),
            ShippedAt = shippingDetailsDto.ShippedAt,
            DeliveredAt = shippingDetailsDto.DeliveredAt
        };
    }
}