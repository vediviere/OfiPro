using Microsoft.EntityFrameworkCore;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Persistence;

public static class ApplicationDbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCategoriesAsync(context);
        await SeedAdminUserAsync(context);
    }

    private static async Task SeedAdminUserAsync(ApplicationDbContext context)
    {
        const string adminEmail = "admin@ofipro.com";

        var adminExists = await context.Users
            .AnyAsync(x => x.Email == adminEmail);

        if (adminExists)
        {
            return;
        }

        var admin = new User
        {
            Id = Guid.NewGuid(),
            Name = "Administrador",
            LastName = "OfiPro",
            Email = adminEmail,
            Phone = "0000000000",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
            State = "N/A",
            City = "N/A",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        admin.UserRoles.Add(new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = admin.Id,
            Role = UserRoleType.Administrador
        });

        await context.Users.AddAsync(admin);

        await context.SaveChangesAsync();
    }

    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        var hasCategories = await context.Categories.AnyAsync();

        if (hasCategories)
        {
            return;
        }

        var constructionId = Guid.NewGuid();
        var electricityId = Guid.NewGuid();
        var plumbingId = Guid.NewGuid();

        var categories = new List<Category>
    {
        new Category
        {
            Id = constructionId,
            Name = "Construcción",
            IsActive = true
        },
        new Category
        {
            Id = electricityId,
            Name = "Electricidad",
            IsActive = true
        },
        new Category
        {
            Id = plumbingId,
            Name = "Plomería",
            IsActive = true
        }
    };

        var subcategories = new List<Subcategory>
    {
        new Subcategory
        {
            Id = Guid.NewGuid(),
            CategoryId = constructionId,
            Name = "Albañilería",
            IsActive = true
        },
        new Subcategory
        {
            Id = Guid.NewGuid(),
            CategoryId = constructionId,
            Name = "Pisos",
            IsActive = true
        },
        new Subcategory
        {
            Id = Guid.NewGuid(),
            CategoryId = constructionId,
            Name = "Bardas",
            IsActive = true
        },
        new Subcategory
        {
            Id = Guid.NewGuid(),
            CategoryId = electricityId,
            Name = "Instalación eléctrica",
            IsActive = true
        },
        new Subcategory
        {
            Id = Guid.NewGuid(),
            CategoryId = electricityId,
            Name = "Reparación eléctrica",
            IsActive = true
        },
        new Subcategory
        {
            Id = Guid.NewGuid(),
            CategoryId = plumbingId,
            Name = "Instalación de tubería",
            IsActive = true
        },
        new Subcategory
        {
            Id = Guid.NewGuid(),
            CategoryId = plumbingId,
            Name = "Reparación de fugas",
            IsActive = true
        }
    };

        await context.Categories.AddRangeAsync(categories);
        await context.Subcategories.AddRangeAsync(subcategories);

        await context.SaveChangesAsync();
    }
}