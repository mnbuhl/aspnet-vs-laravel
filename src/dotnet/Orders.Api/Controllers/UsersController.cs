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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var user = await _repository.Get(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ToDto());
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string email)
    {
        var user = await _repository.GetWithSpecification(new UserByEmailWithOrdersSpec(email));

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto userDto)
    {
        var user = userDto.ToDomain();

        bool created = await _repository.Create(user);

        if (!created)
        {
            return BadRequest("Failed to create user");
        }

        return CreatedAtAction("Get", new { id = user.Id }, user.ToDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto userDto)
    {
        var user = await _repository.Get(id);

        if (user == null)
        {
            return NotFound();
        }

        bool updated = await _repository.Update(user.MapUpdateDto(userDto));

        if (!updated)
        {
            return BadRequest("Failed to update user");
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        bool deleted = await _repository.Delete(id);

        return deleted ? NoContent() : NotFound();
    }
}