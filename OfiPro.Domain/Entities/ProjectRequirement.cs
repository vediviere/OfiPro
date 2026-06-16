namespace OfiPro.Domain.Entities;

public class ProjectRequirement
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid SubcategoryId { get; set; }
    public Subcategory Subcategory { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
}
