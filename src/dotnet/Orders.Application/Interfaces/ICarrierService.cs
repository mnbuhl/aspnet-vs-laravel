namespace Orders.Application.Interfaces;

public interface ICarrierService
{
    bool IsOrderShipped(Guid orderId);
    bool IsOrderDelivered(Guid orderId);
}