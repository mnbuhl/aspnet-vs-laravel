using Orders.Application.Dtos.OrderLines;
using Orders.Application.Dtos.Users;
using Orders.Domain.Models;

namespace Orders.Application.Dtos.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public long Total { get; set; }

    public DateTime Date { get; set; }

    public UserDto? User { get; set; }
    public ShippingDetails? ShippingDetails { get; set; }
    public Address? BillingAddress { get; set; }
    public ICollection<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();
}