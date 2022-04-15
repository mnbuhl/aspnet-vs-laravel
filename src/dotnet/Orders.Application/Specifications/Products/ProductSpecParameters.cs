namespace Orders.Application.Specifications.Products;

public class ProductSpecParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 6;
    private string _sort = "default";

    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string Sort
    {
        get => _sort;
        set => _sort = new[] { "name", "-name", "price", "-price", "created_at", "-created_at" }.Contains(value)
            ? value.ToLower()
            : throw new ArgumentException("Invalid sort option", nameof(value));
    }
}