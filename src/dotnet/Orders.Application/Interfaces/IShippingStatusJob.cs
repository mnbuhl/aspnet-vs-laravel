namespace Orders.Application.Interfaces;

public interface IShippingStatusJob
{
    Task CheckStatus();
}