using Orders.Application.Interfaces;

namespace Orders.Api.Services;

// Service to simulate having a real carrier service to check shipped / delivered status of orders
public class CarrierService : ICarrierService
{
    private static readonly Random Random = new Random();

    public bool IsOrderShipped(Guid orderId)
    {
        return Random.Next(0, orderId.ToString().Length) < orderId.ToString().Length / 4;
    }

    public bool IsOrderDelivered(Guid orderId)
    {
        return Random.Next(0, orderId.ToString().Length) < orderId.ToString().Length / 4;
    }
}