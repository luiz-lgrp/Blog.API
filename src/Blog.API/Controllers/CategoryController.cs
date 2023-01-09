using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version=apiVersion}/[controller]")]
public class CategoryController : ControllerBase
{

    [HttpGet("categories")]
    public async Task<IActionResult> GetAsync([FromServices]BlogDataContext context)
    {
        var categories = await context.Categories.ToListAsync();

        return Ok(categories);
    }
}
