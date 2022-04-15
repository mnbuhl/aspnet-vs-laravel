using Microsoft.AspNetCore.Mvc;
using Orders.Api.Helpers;
using Orders.Application.Dtos.Products;
using Orders.Application.Interfaces;
using Orders.Application.Specifications.Products;
using Orders.Domain.Models;

namespace Orders.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _repository;

    public ProductsController(IRepository<Product> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetAll([FromQuery] ProductSpecParameters parameters)
    {
        var products = await _repository.ListWithSpecification(new ProductsSpecification(parameters));

        if (products.Count == 0)
        {
            return NotFound();
        }

        int productsCount = await _repository.Count(new ProductsSpecification(parameters, count: true));

        return Ok(new PaginatedList<ProductDto>(parameters.PageIndex, parameters.PageSize, productsCount,
            products.Select(x => x.ToDto()!).ToList()));
    }
}