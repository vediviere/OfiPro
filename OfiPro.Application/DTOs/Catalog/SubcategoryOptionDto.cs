namespace OfiPro.Application.DTOs.Catalog;

public class SubcategoryOptionDto
{
    public Guid SubcategoryId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}