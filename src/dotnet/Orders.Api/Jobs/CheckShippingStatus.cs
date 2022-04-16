using Orders.Application.Interfaces;

namespace Orders.Api.Jobs;

public class CheckShippingStatus : IShippingStatusJob
{
    private readonly ILogger<CheckShippingStatus> _logger;

    public CheckShippingStatus(ILogger<CheckShippingStatus> logger)
    {
        _logger = logger;
    }

    public async Task CheckStatus()
    {
        _logger.LogInformation("Running CheckShippingStatus class");
    }
}