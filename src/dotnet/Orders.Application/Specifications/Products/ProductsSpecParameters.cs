using Orders.Application.Helpers;

namespace Orders.Application.Specifications.Products;

public class ProductsSpecParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 6;
    private string? _search;
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
        set => _sort = OrderByValidator.Validate(new[]
        {
            "name", "-name", "price", "-price", "created_at", "-created_at"
        }, value!);
    }

    public string? Search
    {
        get => _search;
        set => _search = value?.ToLower();
    }
}