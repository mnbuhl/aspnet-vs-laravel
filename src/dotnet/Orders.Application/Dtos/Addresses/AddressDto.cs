namespace Orders.Application.Dtos.Addresses;

public class AddressDto
{
    public Guid Id { get; set; }
    public string AddressLine { get; set; } = string.Empty;
    public int ZipCode { get; set; }
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}