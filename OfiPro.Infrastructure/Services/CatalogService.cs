using OfiPro.Application.DTOs.Catalog;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;

namespace OfiPro.Infrastructure.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task<List<CategoryOptionDto>> GetCategoriesAsync()
    {
        var categories = await _catalogRepository.GetActiveCategoriesAsync();

        return categories
            .Select(category => new CategoryOptionDto
            {
                CategoryId = category.Id,
                Name = category.Name
            })
            .ToList();
    }

    public async Task<List<SubcategoryOptionDto>> GetSubcategoriesByCategoryIdAsync(Guid categoryId)
    {
        var subcategories = await _catalogRepository.GetActiveSubcategoriesByCategoryIdAsync(categoryId);

        return subcategories
            .Select(subcategory => new SubcategoryOptionDto
            {
                SubcategoryId = subcategory.Id,
                CategoryId = subcategory.CategoryId,
                Name = subcategory.Name
            })
            .ToList();
    }
}