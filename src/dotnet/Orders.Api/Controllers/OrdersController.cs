using Microsoft.AspNetCore.Mvc;
using Orders.Application.Dtos.Orders;
using Orders.Application.Helpers;
using Orders.Application.Interfaces;
using Orders.Application.Specifications.Orders;
using Orders.Domain.Models;

namespace Orders.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IRepository<Order> _repository;

    public OrdersController(IRepository<Order> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetAll([FromQuery] OrdersSpecParameters parameters)
    {
        var orders = await _repository.ListWithSpecification(new OrdersDefaultSpecification(parameters));
        int ordersCount = await _repository.Count(new OrdersDefaultSpecification(parameters, count: true));

        var paginatedResult = new PaginatedList<OrderDto>(parameters.PageIndex, parameters.PageSize, ordersCount,
            orders.Select(o => o.ToDto()!).ToList());

        return Ok(paginatedResult);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> Get(Guid id)
    {
        var order = await _repository.GetWithSpecification(new OrderWithRelationsSpec(id));

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderDto orderDto)
    {
        var order = orderDto.ToDomain();
        order.CalculateTotal();

        bool created = await _repository.Create(order);

        if (!created)
        {
            return BadRequest("Could not create order");
        }

        return CreatedAtAction("Get", new { id = order.Id }, order.ToDto());
    }
}