using Microsoft.AspNetCore.Mvc;
using Orders.Application.Dtos.Users;
using Orders.Application.Interfaces;
using Orders.Application.Specifications.Users;
using Orders.Domain.Models;

namespace Orders.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _repository;

    public UsersController(IRepository<User> repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        var user = await _repository.Get(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ToDto());
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var user = await _repository.GetWithSpecification(new UserByEmailWithOrdersSpec(email));

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ToDto());
    }
}