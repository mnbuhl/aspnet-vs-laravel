using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Orders.Application.Dtos.Products;
using Orders.Application.Helpers;
using Xunit;

namespace Orders.Api.Test.Integration;

public class ProductsControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetProducts_Returns200Ok()
    {
        var response = await Messenger.Get<PaginatedList<ProductDto>>("products");

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Should().BeOfType<PaginatedList<ProductDto>>();
        response.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task GetProducts_WithPageSize20_Returns20Products()
    {
        var response = await Messenger.Get<PaginatedList<ProductDto>>("products", new { pageSize = 20 });

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Data.Count.Should().Be(20);
        response.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task GetProducts_WithPageIndex2_ShouldNot_BeSameAsPageIndex1()
    {
        var response1 = await Messenger.Get<PaginatedList<ProductDto>>("products", new { pageIndex = 1 });
        var response2 = await Messenger.Get<PaginatedList<ProductDto>>("products", new { pageIndex = 2 });

        response1.Data.Data.Should().NotBeSameAs(response2.Data.Data);
    }

    [Fact]
    public async Task GetProducts_WithSearchParam_ShouldOnly_ReturnMatchingProducts()
    {
        string searchTerm = "10";

        var response =
            await Messenger.Get<PaginatedList<ProductDto>>("products", new { search = searchTerm, pageSize = 50 });

        response.Data.Data.Should().AllSatisfy(x => x.Name.Contains(searchTerm));
    }

    [Fact]
    public async Task GetProduct_WithExistingId_Returns200Ok()
    {
        var product = await GetProduct();

        var response = await Messenger.Get<ProductDto>($"products/{product.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Should().BeOfType<ProductDto>();
        response.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task GetProduct_WithNonExistingId_Returns404NotFound()
    {
        var response = await Messenger.Get<ProductDto>($"products/{Guid.NewGuid()}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        response.Data.Should().BeNull();
        response.ErrorMessage.Should().BeOfType<string>();
    }

    [Fact]
    public async Task CreateProduct_WithValidProduct_Returns201Created()
    {
        var product = new CreateProductDto
        {
            Name = "Test Product",
            Description = "Test Product Description",
            Price = 10000,
            AmountInStock = 100
        };

        var response = await Messenger.Post<CreateProductDto, ProductDto>("products", product);

        response.StatusCode.Should().Be((int)HttpStatusCode.Created);
        response.Data.Name.Should().Be(product.Name);
        response.Data.Description.Should().Be(product.Description);
        response.Data.Price.Should().Be(product.Price);
        response.Data.AmountInStock.Should().Be(product.AmountInStock);
    }

    [Fact]
    public async Task CreateProduct_WithInvalidProduct_Returns400BadRequest()
    {
        var product = new CreateProductDto
        {
            Name = "Failed product"
        };

        var response = await Messenger.Post<CreateProductDto, ProductDto>("products", product);

        response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.ErrorMessage.Should().BeOfType<string>();
    }

    [Fact]
    public async Task UpdateProduct_With_ValidParams_Returns204NoContent()
    {
        var product = await GetProduct();
        var updateProduct = new UpdateProductDto
        {
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 999900,
            AmountInStock = 123
        };

        var response = await Messenger.Put($"products/{product.Id}", updateProduct);
        var updatedProduct = await Messenger.Get<ProductDto>($"products/{product.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        updatedProduct.Data.Name.Should().Be(updateProduct.Name);
        updatedProduct.Data.Description.Should().Be(updateProduct.Description);
        updatedProduct.Data.Price.Should().Be(updateProduct.Price);
        updatedProduct.Data.AmountInStock.Should().Be(updateProduct.AmountInStock);
    }

    [Fact]
    public async Task DeleteProduct_WithExistingId_Returns204NoContent()
    {
        var product = await GetProduct();

        var response = await Messenger.Delete($"products/{product.Id}");

        var deletedProduct = await Messenger.Get<ProductDto>($"products/{product.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        deletedProduct.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteProduct_WithNonExistingId_Returns404NotFound()
    {
        var response = await Messenger.Delete($"products/{Guid.NewGuid()}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    private async Task<ProductDto> GetProduct()
    {
        var response = await Messenger.Get<PaginatedList<ProductDto>>("products");

        return response.Data.Data[0];
    }
}