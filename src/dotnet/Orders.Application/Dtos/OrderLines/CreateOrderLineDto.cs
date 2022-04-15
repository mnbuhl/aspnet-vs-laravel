using System.ComponentModel.DataAnnotations;

namespace Orders.Application.Dtos.OrderLines;

public class CreateOrderLineDto
{
    [Required]
    public long Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int Discount { get; set; }

    [Required]
    public Guid ProductId { get; set; }
}