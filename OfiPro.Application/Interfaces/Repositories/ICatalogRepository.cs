using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface ICatalogRepository
{
    Task<List<Category>> GetActiveCategoriesAsync();

    Task<List<Subcategory>> GetActiveSubcategoriesByCategoryIdAsync(Guid categoryId);
}