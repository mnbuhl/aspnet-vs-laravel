﻿using Microsoft.AspNetCore.Mvc;
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
        var products = await _repository.ListWithSpecification(new ProductsWithOrderByAndPaginationSpec(parameters));

        int productsCount = await _repository.Count(new ProductsWithOrderByAndPaginationSpec(parameters, count: true));

        return Ok(new PaginatedList<ProductDto>(parameters.PageIndex, parameters.PageSize, productsCount,
            products.Select(x => x.ToDto()!).ToList()));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Get(Guid id)
    {
        var product = await _repository.Get(id);

        if (product == null)
            return NotFound();

        return Ok(product.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto productDto)
    {
        var product = productDto.ToDomain();

        bool created = await _repository.Create(product);

        if (!created)
            return BadRequest("Failed to create product");

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product.ToDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductDto productDto)
    {
        var product = await _repository.Get(id);

        if (product == null)
        {
            return NotFound();
        }

        bool updated = await _repository.Update(product.MapUpdateDto(productDto));

        if (!updated)
        {
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