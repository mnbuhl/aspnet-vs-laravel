using Orders.Domain.Models;

namespace Orders.Application.Specifications.Orders;

public class OrdersDefaultSpecification : BaseSpecification<Order>
{
    public OrdersDefaultSpecification(OrdersSpecParameters parameters, bool count = false)
        : base(x => (parameters.UserId == null || x.UserId == parameters.UserId))
    {
        if (count)
        {
            return;
        }

        AddInclude(o => o.BillingAddress!);
        AddInclude(o => o.ShippingDetails!);
        AddInclude(o => o.OrderLines);

        ApplyPagination(parameters.PageSize, parameters.PageIndex);
    }
}