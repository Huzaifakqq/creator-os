using CreatorOS.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CreatorOS.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(ApiResponse<object>.Ok(new
        {
            status = "healthy",
            service = "Creator OS API",
            version = "1.0.0",
            timestamp = DateTime.UtcNow
        }));
    }
}
