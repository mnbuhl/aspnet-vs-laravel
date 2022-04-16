using Orders.Application.Interfaces;
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
        _logger.LogInformation("Running CheckShippingStatus class");
    }
}