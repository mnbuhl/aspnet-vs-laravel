using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Orders.Application.Dtos.Users;
using Xunit;

namespace Orders.Api.Test.Integration;

public class UsersControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetUser_WithExistingId_Returns200Ok()
    {
        var user = await GetUser();

        var response = await Messenger.Get<UserDto>($"users/{user.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Should().BeOfType<UserDto>();
        response.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task GetUser_WithNonExistingId_Returns404NotFound()
    {
        var response = await Messenger.Get<UserDto>($"users/{Guid.NewGuid()}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        response.ErrorMessage.Should().BeOfType<string>();
    }

    [Fact]
    public async Task GetUser_WithExistingEmail_ReturnsUserWithOrders()
    {
        var user = await GetUser();

        var response = await Messenger.Get<UserDto>($"users/email/{user.Email}");

        response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        response.Data.Should().BeOfType<UserDto>();
        response.Data.Orders.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetUser_WithNonExistingEmail_Returns404NotFound()
    {
        var response = await Messenger.Get<UserDto>($"users/email/{Guid.NewGuid()}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        response.ErrorMessage.Should().BeOfType<string>();
    }

    [Fact]
    public async Task CreateUser_WithValidUser_Returns201Created()
    {
        var user = new CreateUserDto()
        {
            Name = "Test User",
            Email = "testuseremail@email.com",
            Phone = "99775544"
        };

        var response = await Messenger.Post<CreateUserDto, UserDto>("users", user);

        response.StatusCode.Should().Be((int)HttpStatusCode.Created);
        response.Data.Should().BeOfType<UserDto>();
        response.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task CreateUser_WithInvalidUser_Returns400BadRequest()
    {
        var user = new CreateUserDto()
        {
            Name = "Test User",
        };

        var response = await Messenger.Post<CreateUserDto, UserDto>("users", user);

        response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.Data.Should().BeNull();
        response.ErrorMessage.Should().BeOfType<string>();
    }

    [Fact]
    public async Task UpdateUser_WithValidUser_Returns204NoContent()
    {
        var user = await GetUser();

        var updateUser = new UpdateUserDto()
        {
            Name = "Test User Updated",
            Phone = "88664422"
        };

        var response = await Messenger.Put($"users/{user.Id}", updateUser);
        var updatedUser = await Messenger.Get<UserDto>($"users/{user.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        response.ErrorMessage.Should().BeNull();

        updatedUser.Data.Name.Should().Be(updateUser.Name);
        updatedUser.Data.Phone.Should().Be(updateUser.Phone);
    }

    [Fact]
    public async Task DeleteUser_WithExistingId_Returns204NoContent()
    {
        var user = await GetUser();

        var response = await Messenger.Delete($"users/{user.Id}");
        var deletedUser = await Messenger.Get<UserDto>($"users/{user.Id}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        response.ErrorMessage.Should().BeNull();
        deletedUser.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteUser_WithNonExistingId_Returns404NotFound()
    {
        var response = await Messenger.Delete($"users/{Guid.NewGuid()}");

        response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        response.ErrorMessage.Should().BeOfType<string>();
    }

    private async Task<UserDto> GetUser()
    {
        var response = await Messenger.Get<UserDto>("users/email/user20@email.com");
        return response.Data;
    }
}