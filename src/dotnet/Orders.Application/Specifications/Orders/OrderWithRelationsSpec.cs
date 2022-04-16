using Orders.Domain.Models;

namespace Orders.Application.Specifications.Orders;

public class OrderWithRelationsSpec : BaseSpecification<Order>
{
    public OrderWithRelationsSpec(Guid id) : base(x => x.Id == id)
    {
        AddInclude(x => x.OrderLines);
        AddThenInclude("OrderLines.Product");
        AddInclude(x => x.BillingAddress);
        AddInclude(x => x.ShippingDetails);
        AddInclude(x => x.User);
    }
}