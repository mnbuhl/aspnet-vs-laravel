using Orders.Application.Interfaces;
using Orders.Application.Specifications.Orders;
using Orders.Domain.Models;

namespace Orders.Api.Jobs;

public class CheckShippingStatus : IShippingStatusJob
{
    private readonly ICarrierService _carrierService;
    private readonly ILogger<CheckShippingStatus> _logger;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<ShippingDetails> _shippingDetailsRepository;

    public CheckShippingStatus(ILogger<CheckShippingStatus> logger, IRepository<Order> orderRepository,
        IRepository<ShippingDetails> shippingDetailsRepository, ICarrierService carrierService)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _shippingDetailsRepository = shippingDetailsRepository;
        _carrierService = carrierService;
    }

    public async Task CheckStatus()
    {
        int ordersShipped = 0;
        int ordersDelivered = 0;

        var notShippedOrders = await _orderRepository.ListWithSpecification(new OrdersThatAreNotShippedSpec());
        var notDeliveredOrders = await _orderRepository.ListWithSpecification(new OrdersThatAreNotDeliveredSpec());

        foreach (var order in notShippedOrders)
        {
            if (!_carrierService.IsOrderShipped(order.Id))
                continue;

            order.ShippingDetails!.ShippedAt = DateTime.UtcNow;
            await _shippingDetailsRepository.Update(order.ShippingDetails);
            ordersShipped++;
        }

        foreach (var order in notDeliveredOrders)
        {
            if (!_carrierService.IsOrderDelivered(order.Id))
                continue;

            order.ShippingDetails!.DeliveredAt = DateTime.UtcNow;
            await _shippingDetailsRepository.Update(order.ShippingDetails);
            ordersDelivered++;
        }

        _logger.LogInformation("Amount of orders shipped: {OrdersShipped} out of {TotalOrders}", ordersShipped,
            notShippedOrders.Count);
        _logger.LogInformation("Amount of orders delivered: {OrdersDelivered} out of {TotalOrders}", ordersDelivered,
            notDeliveredOrders.Count);
    }
}