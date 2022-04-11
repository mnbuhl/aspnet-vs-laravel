using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Domain.Models;

public class ShippingDetails : BaseEntity
{
    public string Carrier { get; set; } = string.Empty;
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }

    [ForeignKey(nameof(ShippingAddress))]
    public Guid ShippingAddressId { get; set; }
    public Address? ShippingAddress { get; set; }
}