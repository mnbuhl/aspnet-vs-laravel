using Microsoft.AspNetCore.Mvc;
using Orders.Application.Dtos.Products;
using Orders.Application.Helpers;
using Orders.Application.Interfaces;
using Orders.Application.Specifications.Products;
using Orders.Domain.Models;

namespace Orders.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IRepository<Product> _repository;

    public ProductsController(IRepository<Product> repository, ILogger<ProductsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetAll([FromQuery] ProductsSpecParameters parameters)
    {
        var products = await _repository.ListWithSpecification(new ProductsDefaultSpecification(parameters));
        int productsCount = await _repository.Count(new ProductsDefaultSpecification(parameters, count: true));

        return Ok(new PaginatedList<ProductDto>(parameters.PageIndex, parameters.PageSize, productsCount,
            products.Select(x => x.ToDto()!).ToList()));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Get(Guid id)
    {
        var product = await _repository.Get(id);

        if (product == null)
        {
            _logger.LogInformation("Product with id {Id} not found", id);
            return NotFound();
        }

        return Ok(product.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto productDto)
    {
        var product = productDto.ToDomain();

        bool created = await _repository.Create(product);

        if (!created)
        {
            _logger.LogInformation("Failed to create Product");
            return BadRequest("Failed to create product");
        }

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product.ToDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductDto productDto)
    {
        var product = await _repository.Get(id);

        if (product == null)
        {
            _logger.LogInformation("Product with id {Id} not found", id);
            return NotFound();
        }

        bool updated = await _repository.Update(product.MapUpdateDto(productDto));

        if (!updated)
        {
            _logger.LogInformation("Product with id {Id} not updated", product.Id);
            return BadRequest("Failed to update product");
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _repository.Delete(id);

        return deleted ? NoContent() : NotFound();
    }
}