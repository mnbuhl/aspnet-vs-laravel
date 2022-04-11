using System.ComponentModel.DataAnnotations;

namespace Orders.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public long Price { get; set; }
    public int AmountInStock { get; set; }
    public string Description { get; set; } = string.Empty;
}