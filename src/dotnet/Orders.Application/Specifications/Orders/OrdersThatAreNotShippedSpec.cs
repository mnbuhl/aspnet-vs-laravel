using Orders.Domain.Models;

namespace Orders.Application.Specifications.Orders;

public class OrdersThatAreNotShippedSpec : BaseSpecification<Order>
{
    public OrdersThatAreNotShippedSpec()
        : base(x => x.ShippingDetails != null && x.ShippingDetails.ShippedAt == null)
    {
        AddInclude(x => x.ShippingDetails);
    }
}