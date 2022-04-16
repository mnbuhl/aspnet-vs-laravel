using Orders.Domain.Models;

namespace Orders.Application.Specifications.Orders;

public class OrdersThatAreNotDeliveredSpec : BaseSpecification<Order>
{
    public OrdersThatAreNotDeliveredSpec()
        : base(
            x => x.ShippingDetails != null
                 && x.ShippingDetails.ShippedAt != null
                 && x.ShippingDetails.DeliveredAt == null
                 && x.ShippingDetails.ShippedAt.Value.AddDays(3) < DateTime.UtcNow
        )
    {
        AddInclude(x => x.ShippingDetails);
    }
}