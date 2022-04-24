using Microsoft.AspNetCore.Mvc;

namespace Orders.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DemoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test 3");
    }
}