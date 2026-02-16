using Category.Application.Exceptions;
using Category.Domain.Abstractions.Services;
using HelpDesk.Contracts.Requests.Category;
using Microsoft.AspNetCore.Mvc;

namespace Category.Presentation.Controllers;

/// <summary>
///     Контроллер Category
/// </summary>
[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? id, int page = 0, int limit = 20)
    {
        try
        {
            var response = await _categoryService.Get(id, page, limit);
            return Ok(response);
        }
        catch (CategoryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostCategoryRequest request)
    {
        try
        {
            var response = await _categoryService.Create(request);
            return Ok(response);
        }
        catch (CategoryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] PatchCategoryRequest request)
    {
        try
        {
            var response = await _categoryService.Update(request);
            return Ok(response);
        }
        catch (CategoryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _categoryService.Delete(id);
            return Ok(response);
        }
        catch (CategoryException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}