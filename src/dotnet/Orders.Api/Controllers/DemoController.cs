using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Infrastructure.Data;

namespace Orders.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DemoController : ControllerBase
{
    private readonly AppDbContext _context;

    public DemoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test 3");
    }

    [HttpPost]
    public async Task<IActionResult> ClearDatabase()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.Products.RemoveRange(_context.Products);
        _context.Addresses.RemoveRange(_context.Addresses);
        _context.ShippingDetails.RemoveRange(_context.ShippingDetails);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}