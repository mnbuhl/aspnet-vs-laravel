using Orders.Application.Helpers;

namespace Orders.Application.Specifications.Orders;

public class OrdersSpecParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 6;
    private string? _sort;

    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string? Sort
    {
        get => _sort;
        set => _sort = OrderByValidator.Validate(new[] { "total", "-total", "date", "-date" }, value);
    }

    public Guid? UserId { get; set; }
}