using OfiPro.Application.DTOs.Catalog;

namespace OfiPro.Application.Interfaces.Services;

public interface ICatalogService
{
    Task<List<CategoryOptionDto>> GetCategoriesAsync();

    Task<List<SubcategoryOptionDto>> GetSubcategoriesByCategoryIdAsync(Guid categoryId);
}