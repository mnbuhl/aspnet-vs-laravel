using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Orders.Application.Dtos.Addresses;
using Orders.Application.Dtos.OrderLines;
using Orders.Application.Dtos.Orders;
using Orders.Application.Dtos.Products;
using Orders.Application.Dtos.ShippingDetail;
using Orders.Application.Dtos.Users;
using Orders.Application.Helpers;
using Xunit;

namespace Orders.Api.Test.Integration;

[Collection("Integration")]
public class OrdersControllerTests : IntegrationTest
{
    private static readonly Random Random = new Random();

    [Fact]
    public async Task GetOrders_WithNoParameters_Returns200Ok()
    {
        (var response, bool _, int statusCode) = await Messenger.Get<PaginatedList<OrderDto>>("orders");

        statusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Should().BeOfType<List<OrderDto>>();
        response.Data.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetOrders_WithUserIdParameter_ReturnsOrdersForUser()
    {
        var user = await GetUser();

        var response = await Messenger.Get<PaginatedList<OrderDto>>("orders", new { userId = user.Id });

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Data.Should().BeOfType<List<OrderDto>>();
        response.Data.Data.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetOrder_WithValidId_ReturnsOrderWithRelations()
    {
        var order = await GetOrder();

        var response = await Messenger.Get<OrderDto>($"orders/{order.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Should().BeOfType<OrderDto>();
        response.Data.BillingAddress.Should().NotBeNull();
        response.Data.ShippingDetails.Should().NotBeNull();
        response.Data.User.Should().NotBeNull();
        response.Data.OrderLines.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrder_WithInvalidId_Returns404NotFound()
    {
        var response = await Messenger.Get<OrderDto>($"orders/{Guid.NewGuid()}");

        response.Data.Should().BeNull();
        response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        response.ErrorMessage.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateOrder_WithValidProperties_Returns201Created()
    {
        var user = await GetUser();
        var products = await GetProducts();

        var order = new CreateOrderDto
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            BillingAddress = new CreateAddressDto
            {
                AddressLine = "Test Rd 1",
                City = "Test City",
                ZipCode = 5544,
                Country = "Test Country"
            },
            ShippingDetails = new CreateShippingDetailsDto
            {
                Carrier = "Test Carrier",
                ShippedAt = DateTime.UtcNow,
                ShippingAddress = new CreateAddressDto
                {
                    AddressLine = "Test Rd 5",
                    City = "Test City",
                    ZipCode = 5544,
                    Country = "Test Country"
                }
            },
            OrderLines = new List<CreateOrderLineDto>
            {
                new CreateOrderLineDto
                {
                    ProductId = products[0].Id,
                    Price = products[0].Price,
                    Quantity = 1,
                    Discount = 20
                },
                new CreateOrderLineDto
                {
                    ProductId = products[1].Id,
                    Price = products[1].Price,
                    Quantity = 1,
                    Discount = 0
                }
            }
        };

        var response = await Messenger.Post<CreateOrderDto, OrderDto>("orders", order);

        response.StatusCode.Should().Be((int)HttpStatusCode.Created);
        response.Data.Should().BeOfType<OrderDto>();
        response.Data.Total.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task CreateOrder_WithInvalidProperties_Returns400BadRequest()
    {
        var order = new CreateOrderDto();

        var response = await Messenger.Post<CreateOrderDto, OrderDto>("orders", order);

        response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.ErrorMessage.Should().BeOfType<string>();
        response.Data.Should().BeNull();
    }

    [Fact]
    public async Task DeleteOrder_WithNoShippedAt_Returns204NoContent()
    {
        var user = await GetUser();
        var products = await GetProducts();

        var orderDto = new CreateOrderDto
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            BillingAddress = new CreateAddressDto
            {
                AddressLine = "Test Rd 1",
                City = "Test City",
                ZipCode = 5544,
                Country = "Test Country"
            },
            ShippingDetails = new CreateShippingDetailsDto
            {
                Carrier = "Test Carrier",
                ShippingAddress = new CreateAddressDto
                {
                    AddressLine = "Test Rd 5",
                    City = "Test City",
                    ZipCode = 5544,
                    Country = "Test Country"
                }
            },
            OrderLines = new List<CreateOrderLineDto>
            {
                new CreateOrderLineDto
                {
                    ProductId = products[0].Id,
                    Price = products[0].Price,
                    Quantity = 1,
                    Discount = 20
                },
                new CreateOrderLineDto
                {
                    ProductId = products[1].Id,
                    Price = products[1].Price,
                    Quantity = 1,
                    Discount = 0
                }
            }
        };

        var order = await Messenger.Post<CreateOrderDto, OrderDto>("orders", orderDto);

        var response = await Messenger.Delete($"orders/{order.Data.Id}");
        var responseAfterDelete = await Messenger.Get<OrderDto>($"orders/{order.Data.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        response.ErrorMessage.Should().BeNull();
        responseAfterDelete.Data.Should().BeNull();
        responseAfterDelete.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteOrder_WithShippedAt_Returns400BadRequest()
    {
        var order = await GetOrder();

        var response = await Messenger.Delete($"orders/{order.Id}");
        var orderResponse = await Messenger.Get<OrderDto>($"orders/{order.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.ErrorMessage.Should().BeOfType<string>();
        orderResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
        orderResponse.Data.Should().BeOfType<OrderDto>();
    }

    private async Task<OrderDto> GetOrder()
    {
        var response = await Messenger.Get<PaginatedList<OrderDto>>("orders");

        return response.Data.Data[0];
    }

    private async Task<UserDto> GetUser()
    {
        var response = await Messenger.Get<UserDto>($"users/email/user{Random.Next(0, 100)}@email.com");

        if (response.Data.Orders.Count != 0)
        {
            return response.Data;
        }

        return await GetUser();
    }

    private async Task<IReadOnlyList<ProductDto>> GetProducts()
    {
        var response = await Messenger.Get<PaginatedList<ProductDto>>("products");

        return response.Data.Data;
    }
}