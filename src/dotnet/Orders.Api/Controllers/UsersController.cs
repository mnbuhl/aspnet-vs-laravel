using Microsoft.AspNetCore.Mvc;
using Orders.Application.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        return Ok();
    }
}