using Orders.Application.Dtos.Addresses;
using Orders.Application.Dtos.OrderLines;
using Orders.Application.Dtos.ShippingDetail;
using Orders.Application.Dtos.Users;

namespace Orders.Application.Dtos.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public long Total { get; set; }

    public DateTime Date { get; set; }

    public UserDto? User { get; set; }
    public ShippingDetailsDto? ShippingDetails { get; set; }
    public AddressDto? BillingAddress { get; set; }
    public List<OrderLineDto?> OrderLines { get; set; } = new List<OrderLineDto?>();
}