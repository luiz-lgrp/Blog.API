using Blog.API.ViewModels;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version=apiVersion}/[controller]")]
public class CategoryController : ControllerBase
{

    private readonly BlogDataContext _blogDataContext;

    public CategoryController(BlogDataContext blogDataContext)
    {
        _blogDataContext = blogDataContext;
    }


    [HttpGet("categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        try
        {
            //var categories = await _blogDataContext.Categories.ToListAsync();
            var categories = await context.Categories.ToListAsync();

            return Ok(new Response<List<Category>>(categories));
        }
        catch 
        {
            return StatusCode(500, new Response<List<Category>>("Falha interna no servidor."));
        }
    }  
    
    [HttpGet("categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            var category = await _blogDataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
                return NotFound();

            return Ok(category);
        }
        catch 
        {
            return StatusCode(500, new Response<List<Category>>("Falha interna no servidor."));
        }
    }

    [HttpPost("categories")]
    public async Task<IActionResult> PostAsync([FromBody] CreateCategoryViewModel model)
    {
        try
        {
            var category = new Category
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower(),
            };

            await _blogDataContext.Categories.AddAsync(category);
            await _blogDataContext.SaveChangesAsync();

            return Created($"categories/{category.Id}, {category.Name}", category);
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new Response<List<Category>>("Não foi possível incluir a categoria."));
        }
        catch 
        {
            return StatusCode(500, new Response<List<Category>>("Falha interna no servidor."));
        }
    }

    [HttpPut("categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] UpdateCategoryViewModel model)
    {
        try
        {
            var category = await _blogDataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;

            _blogDataContext.Categories.Update(category);
            await _blogDataContext.SaveChangesAsync();

            return Ok(category);
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new Response<List<Category>>("Não foi possível alterar a categoria."));
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<List<Category>>("Falha interna no servidor."));
        }

    }

    [HttpDelete("categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            var category = await _blogDataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
                return NotFound();

            _blogDataContext.Categories.Remove(category);
            await _blogDataContext.SaveChangesAsync();

            return Ok(category);
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new Response<List<Category>>("Não foi possível remover a categoria."));
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<List<Category>>("Falha interna no servidor."));
        }
    }

}
