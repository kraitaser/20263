namespace _20263.Controllers;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using _20263.Context;
using _20263.DTOs.Categories;
using _20263.Model;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper mapper;
    public CategoriesController(ApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this.mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<List<CategoryDTO>>> Get()
    {
        var categories = await _context.Categories.ToListAsync();
        return mapper.Map<List<CategoryDTO>>(categories);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var categories = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (categories == null)
        {
            return NotFound();
        }
        return mapper.Map<CategoryDTO>(categories);
    }
    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Create(CategoryCreateDto categoryCreateDto)
    {
        var category = mapper.Map<Category>(categoryCreateDto);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        var response = mapper.Map<CategoryDTO>(category);
        return CreatedAtAction(nameof(Get), new { id = category.Id }, response);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDto categoryUpdateDto)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        mapper.Map(categoryUpdateDto, category);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    }