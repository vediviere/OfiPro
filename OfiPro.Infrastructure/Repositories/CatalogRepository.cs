using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly ApplicationDbContext _context;

    public CatalogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetActiveCategoriesAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<List<Subcategory>> GetActiveSubcategoriesByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Subcategories
            .AsNoTracking()
            .Where(x => x.IsActive && x.CategoryId == categoryId)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}