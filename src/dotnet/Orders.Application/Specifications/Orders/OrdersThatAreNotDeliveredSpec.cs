using Orders.Domain.Models;

namespace Orders.Application.Specifications.Orders;

public class OrdersThatAreNotDeliveredSpec : BaseSpecification<Order>
{
    public OrdersThatAreNotDeliveredSpec()
        : base(
            x => x.ShippingDetails != null
                 && x.ShippingDetails.ShippedAt != null
                 && x.ShippingDetails.DeliveredAt == null
        )
    {
        AddInclude(x => x.ShippingDetails);
    }
}