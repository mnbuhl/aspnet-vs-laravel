using System.ComponentModel.DataAnnotations;

namespace Orders.Domain.Models;

public class Address : BaseEntity
{
    public string AddressLine { get; set; } = string.Empty;
    public int ZipCode { get; set; }
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}