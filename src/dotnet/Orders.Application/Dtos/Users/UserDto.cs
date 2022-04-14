using Orders.Application.Dtos.Orders;

namespace Orders.Application.Dtos.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<OrderDto?> Orders { get; set; } = new List<OrderDto?>();
}