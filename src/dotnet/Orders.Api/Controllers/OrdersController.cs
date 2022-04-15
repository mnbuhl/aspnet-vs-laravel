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
    private readonly IDatabaseTransaction _databaseTransaction;
    private readonly ILogger<OrdersController> _logger;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;

    public OrdersController(IRepository<Order> orderRepository, IRepository<Product> productRepository,
        IDatabaseTransaction databaseTransaction, ILogger<OrdersController> logger)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _databaseTransaction = databaseTransaction;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetAll([FromQuery] OrdersSpecParameters parameters)
    {
        var orders = await _orderRepository.ListWithSpecification(new OrdersDefaultSpecification(parameters));
        int ordersCount = await _orderRepository.Count(new OrdersDefaultSpecification(parameters, count: true));

        var paginatedResult = new PaginatedList<OrderDto>(parameters.PageIndex, parameters.PageSize, ordersCount,
            orders.Select(o => o.ToDto()!).ToList());

        return Ok(paginatedResult);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> Get(Guid id)
    {
        var order = await _orderRepository.GetWithSpecification(new OrderWithRelationsSpec(id));

        if (order == null)
        {
            _logger.LogInformation("Order with id: {Id} not found", id);
            return NotFound();
        }

        return Ok(order.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderDto orderDto)
    {
        var order = orderDto.ToDomain();
        order.CalculateTotal();

        await _databaseTransaction.BeginTransaction();

        foreach (var line in order.OrderLines)
        {
            var product = await _productRepository.Get(line.ProductId);

            if (product == null)
                return BadRequest("Product on order line not found");

            try
            {
                product.UpdateQuantity(line.Quantity);
            }
            catch (Exception e)
            {
                _logger.LogError("Error updating product quantity of product: {Id}. Error: {Error}",
                    product.Id, e.Message);
                await _databaseTransaction.RollbackTransaction();
                return BadRequest("Could not update product quantity");
            }

            await _productRepository.Update(product);
        }

        bool created = await _orderRepository.Create(order);

        if (!created)
        {
            _logger.LogCritical("Could not create order. Rolling back transaction");
            await _databaseTransaction.RollbackTransaction();
            return BadRequest("Could not create order");
        }

        await _databaseTransaction.CommitTransaction();

        return CreatedAtAction("Get", new { id = order.Id }, order.ToDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var order = await _orderRepository.GetWithSpecification(new OrderWithRelationsSpec(id));

        if (order == null)
        {
            _logger.LogInformation("Order with id: {Id} not found and could therefore not be deleted", id);
            return NotFound();
        }

        if (order.ShippingDetails?.ShippedAt != null)
        {
            _logger.LogInformation("Order with id: {Id} has already been shipped and could therefore not be deleted",
                id);
            return BadRequest("Order has already been shipped");
        }

        await _databaseTransaction.BeginTransaction();

        foreach (var line in order.OrderLines)
        {
            var product = await _productRepository.Get(line.ProductId);

            if (product == null)
            {
                _logger.LogInformation("Product on order line not found");
                return BadRequest("Product on order line not found");
            }

            try
            {
                product.UpdateQuantity(line.Quantity * -1);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Failed to update product quantity of product: {Id}. Error: {Error}",
                    product.Id, e.Message);
                await _databaseTransaction.RollbackTransaction();
                return BadRequest("Could not update product quantity");
            }

            await _productRepository.Update(product);
        }

        bool deleted = await _orderRepository.Delete(id);

        if (!deleted)
        {
            _logger.LogCritical("Could not delete order. Rolling back transaction");
            await _databaseTransaction.RollbackTransaction();
            return BadRequest("Could not delete order");
        }

        await _databaseTransaction.CommitTransaction();

        return NoContent();
    }
}