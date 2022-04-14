using Orders.Application.Dtos.Orders;
using Orders.Domain.Models;

namespace Orders.Application.Dtos.Users;

public static class UserMappings
{
    public static UserDto? ToDto(this User? user)
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
            Orders = user.Orders.Select(o => o.ToDto(false)).ToList(),
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}