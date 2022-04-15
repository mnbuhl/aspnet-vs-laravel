using System.ComponentModel.DataAnnotations;

namespace Orders.Application.Dtos.Addresses;

public class CreateAddressDto
{
    [Required]
    public string AddressLine { get; set; } = string.Empty;

    [Required]
    public int ZipCode { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;
}