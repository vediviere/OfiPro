using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Persistence;

public static class ApplicationDbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
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
}