using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.Interfaces.Services;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _catalogService.GetCategoriesAsync();

        return Ok(categories);
    }

    [HttpGet("categories/{categoryId:guid}/subcategories")]
    public async Task<IActionResult> GetSubcategoriesByCategoryId(Guid categoryId)
    {
        var subcategories = await _catalogService.GetSubcategoriesByCategoryIdAsync(categoryId);

        return Ok(subcategories);
    }
}