using Orders.Application.Dtos.Addresses;

namespace Orders.Application.Dtos.ShippingDetail;

public class ShippingDetailsDto
{
    public Guid Id { get; set; }
    public string Carrier { get; set; } = string.Empty;
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public AddressDto? ShippingAddress { get; set; }
}