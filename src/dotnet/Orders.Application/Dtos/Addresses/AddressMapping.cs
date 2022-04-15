using Orders.Domain.Models;

namespace Orders.Application.Dtos.Addresses;

public static class AddressMapping
{
    public static AddressDto? ToDto(this Address? address)
    {
        if (address == null)
            return null;

        return new AddressDto
        {
            Id = address.Id,
            AddressLine = address.AddressLine,
            ZipCode = address.ZipCode,
            City = address.City,
            Country = address.Country
        };
    }

    public static Address ToDomain(this CreateAddressDto addressDto)
    {
        return new Address
        {
            AddressLine = addressDto.AddressLine,
            City = addressDto.City,
            ZipCode = addressDto.ZipCode,
            Country = addressDto.Country
        };
    }
}