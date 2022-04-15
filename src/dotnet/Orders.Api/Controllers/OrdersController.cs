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

    // In a real system this would be for admins only
    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetAll([FromQuery] OrdersSpecParameters parameters)
    {
        return Ok();
    }
}