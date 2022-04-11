using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Domain.Models;

public class OrderLine : BaseEntity
{
    public long Price { get; set; }
    public int Quantity { get; set; }
    [Range(0, 100)]
    public int Discount { get; set; }

    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}