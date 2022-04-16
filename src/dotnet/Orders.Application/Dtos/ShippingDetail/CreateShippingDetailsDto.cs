using System.ComponentModel.DataAnnotations;
using Orders.Application.Dtos.Addresses;

namespace Orders.Application.Dtos.ShippingDetail;

public class CreateShippingDetailsDto
{
    [Required]
    public string Carrier { get; set; } = string.Empty;

    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }

    public Guid ShippingAddressId { get; set; }
    public CreateAddressDto? ShippingAddress { get; set; }
}