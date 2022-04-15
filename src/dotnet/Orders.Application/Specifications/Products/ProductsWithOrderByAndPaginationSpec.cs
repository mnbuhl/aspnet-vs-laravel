using Orders.Domain.Models;

namespace Orders.Application.Specifications.Products;

public class ProductsWithOrderByAndPaginationSpec : BaseSpecification<Product>
{
    public ProductsWithOrderByAndPaginationSpec(ProductSpecParameters parameters, bool count = false)
        : base(x => string.IsNullOrEmpty(parameters.Search) || x.Name.ToLower().Contains(parameters.Search))
    {
        if (count)
            return;

        ApplyPagination(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);

        switch (parameters.Sort)
        {
            case "name":
                AddOrderBy(p => p.Name);
                break;
            case "-name":
                AddOrderByDescending(p => p.Name);
                break;
            case "price":
                AddOrderBy(p => p.Price);
                break;
            case "-price":
                AddOrderByDescending(p => p.Price);
                break;
            case "created_at":
                AddOrderBy(p => p.CreatedAt);
                break;
            default:
                AddOrderByDescending(p => p.CreatedAt);
                break;
        }
    }
}