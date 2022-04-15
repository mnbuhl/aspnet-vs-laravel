using Orders.Application.Dtos.Orders;
using Orders.Domain.Models;

namespace Orders.Application.Dtos.Users;

public static class UserMappings
{
    public static UserDto? ToDto(this User? user, bool mapOrders = true)
    {
        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Orders = mapOrders ? user.Orders.Select(o => o.ToDto(false)).ToList() : new List<OrderDto?>(),
        };
    }

    public static User ToDomain(this CreateUserDto userDto)
    {
        return new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Phone = userDto.Phone
        };
    }

    public static User MapUpdateDto(this User user, UpdateUserDto userDto)
    {
        user.Name = userDto.Name ?? user.Name;
        user.Phone = userDto.Phone ?? user.Phone;

        return user;
    }
}