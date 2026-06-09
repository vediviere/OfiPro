using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfiPro.Domain.Entities;

namespace OfiPro.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<ProfessionalProfile> ProfessionalProfiles => Set<ProfessionalProfile>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Subcategory> Subcategories => Set<Subcategory>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectRequirement> ProjectRequirements => Set<ProjectRequirement>();
    public DbSet<ProjectPhoto> ProjectPhotos => Set<ProjectPhoto>();
    public DbSet<Proposal> Proposals => Set<Proposal>();
    public DbSet<Evidence> Evidences => Set<Evidence>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Invitation> Invitations => Set<Invitation>();
}
