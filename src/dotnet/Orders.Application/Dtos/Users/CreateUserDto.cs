using System.ComponentModel.DataAnnotations;

namespace Orders.Application.Dtos.Users;

public class CreateUserDto
{
    public Guid? Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;
}