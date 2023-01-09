using Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version=apiVersion}/[controller]")]
public class CategoryController : ControllerBase
{

    [HttpGet("categories")]
    public IActionResult Get([FromServices]BlogDataContext context)
    {
        var categories = context.Categories.ToList();

        return Ok(categories);
    }
}
