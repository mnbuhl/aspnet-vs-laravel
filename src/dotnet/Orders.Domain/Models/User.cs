using System.ComponentModel.DataAnnotations;

namespace Orders.Domain.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}