using System.ComponentModel.DataAnnotations;
using Orders.Application.Dtos.Addresses;
using Orders.Application.Dtos.OrderLines;
using Orders.Application.Dtos.ShippingDetail;

namespace Orders.Application.Dtos.Orders;

public class CreateOrderDto
{
    public Guid? Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public CreateShippingDetailsDto? ShippingDetails { get; set; }

    public Guid BillingAddressId { get; set; }
    public CreateAddressDto? BillingAddress { get; set; }
    public List<CreateOrderLineDto> OrderLines { get; set; } = new List<CreateOrderLineDto>();
}