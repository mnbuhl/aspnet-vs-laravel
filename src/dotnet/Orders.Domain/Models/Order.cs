using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Domain.Models;

public class Order : BaseEntity
{
    public long Total { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    [ForeignKey(nameof(BillingAddress))]
    public Guid BillingAddressId { get; set; }
    public Address? BillingAddress { get; set; }

    [ForeignKey(nameof(ShippingDetails))]
    public Guid ShippingDetailsId { get; set; }
    public ShippingDetails? ShippingDetails { get; set; }
}