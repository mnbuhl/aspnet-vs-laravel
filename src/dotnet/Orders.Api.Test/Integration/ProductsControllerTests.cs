using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Orders.Application.Helpers;
using Orders.Domain.Models;
using Xunit;

namespace Orders.Api.Test.Integration;

public class ProductsControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetProducts_ShouldReturn_200Ok()
    {
        var response = await Messenger.Get<PaginatedList<Product>>("products");

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Should().BeOfType<PaginatedList<Product>>();
        response.ErrorMessage.Should().BeNull();
    }
}