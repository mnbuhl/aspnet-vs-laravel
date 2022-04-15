using Orders.Application.Dtos.Addresses;
using Orders.Application.Dtos.OrderLines;
using Orders.Application.Dtos.ShippingDetail;

namespace Orders.Application.Dtos.Orders;

public class CreateOrderDto
{
    public DateTime Date { get; set; }
    public Guid UserId { get; set; }
    public CreateShippingDetailsDto? ShippingDetails { get; set; }
    public CreateAddressDto? BillingAddress { get; set; }
    public List<CreateOrderLineDto> OrderLines { get; set; } = new List<CreateOrderLineDto>();
}