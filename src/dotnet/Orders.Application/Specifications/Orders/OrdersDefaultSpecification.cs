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

        AddInclude(o => o.BillingAddress);
        AddInclude(o => o.ShippingDetails);
        AddInclude(o => o.OrderLines);

        if (parameters.UserId == null)
        {
            AddInclude(o => o.User);
        }

        ApplyPagination(parameters.PageSize, parameters.PageIndex);

        switch (parameters.Sort)
        {
            case "total":
                AddOrderBy(o => o.Total);
                break;
            case "-total":
                AddOrderByDescending(o => o.Total);
                break;
            case "date":
                AddOrderBy(o => o.Date as DateTime?);
                break;
            case "-date":
                AddOrderByDescending(o => o.Date as DateTime?);
                break;
            default:
                AddOrderByDescending(o => o.CreatedAt as DateTime?);
                break;
        }
    }
}